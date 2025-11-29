using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;              // <--- important for Dapper extension methods
using SAConstruction.Models;
using SAConstruction.DTO;
using BCrypt.Net;

namespace SAConstruction.Repositories
{
    public class UserRepository
    {
        private readonly DataContextDapper _dapper;
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _connectionString = config["sql-connection-string"]
                ?? throw new Exception("Connection string 'sql-connection-string' is missing.");
        }

        public User? GetUserByEmail(string email)
        {
            const string sql = @"
                SELECT TOP 1 
                    UserId,
                    Email,
                    FirstName,
                    LastName,
                    PasswordHash,
                    DateCreated,
                    UpdatedAt,
                    LastPasswordResetEmailSentAt,
                    LastLogin
                FROM Users.AccountData
                WHERE Email = @Email;
            ";

            var users = _dapper.LoadData<User>(sql, new { Email = email });
            return users.FirstOrDefault();
        }

        public User GetUserById(int userId)
        {
            const string sql = @"
                SELECT TOP 1 
                    UserId,
                    Email,
                    FirstName,
                    LastName,
                    PasswordHash,
                    DateCreated,
                    UpdatedAt,
                    LastPasswordResetEmailSentAt,
                    LastLogin
                FROM Users.AccountData
                WHERE UserId = @UserId;
            ";

            var users = _dapper.LoadData<User>(sql, new { UserId = userId });
            var user = users.FirstOrDefault();

            if (user == null)
            {
                throw new Exception($"User with id {userId} not found.");
            }

            return user;
        }


        public UserWithPermissions GetUserWithPermissionsById(int userId)
        {
            const string sql = @"
                SELECT 
                    u.UserId,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.PasswordHash,
                    u.DateCreated,
                    u.UpdatedAt,
                    u.LastPasswordResetEmailSentAt,
                    COALESCE(p.JobPostings, 0)         AS JobPostings,
                    COALESCE(p.AccountManagement, 0)   AS AccountManagement,
                    COALESCE(p.ViewCandidates, 0)      AS ViewCandidates
                FROM Users.AccountData AS u
                LEFT JOIN Users.UserPermissions AS p
                    ON u.UserId = p.UserId
                WHERE u.UserId = @UserId;
            ";

            var results = _dapper.LoadData<UserWithPermissions>(
                sql,
                new { UserId = userId }
            );

            var user = results.FirstOrDefault();
            if (user == null)
                throw new Exception($"User with id {userId} not found.");

            return user;
        }

        public UserWithPermissions CreateUser(CreateUserRequest request)
        {
            // generate random password
            string randomPassword = GenerateRandomPassword(10);

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                const string insertAccountSql = @"
                    INSERT INTO Users.AccountData
                        (Email, FirstName, LastName, PasswordHash, DateCreated, UpdatedAt)
                    VALUES
                        (@Email, @FirstName, @LastName, @PasswordHash, @Now, @Now);
                    SELECT CAST(SCOPE_IDENTITY() AS int);
                ";

                var newUserId = conn.QuerySingle<int>(
                    insertAccountSql,
                    new {
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PasswordHash = randomPassword,  // store unhashed
                        Now = DateTime.UtcNow
                    },
                    transaction: tran
                );

                const string insertPermSql = @"
                    INSERT INTO Users.UserPermissions
                        (UserId, JobPostings, AccountManagement, ViewCandidates)
                    VALUES
                        (@UserId, @JobPostings, @AccountManagement, @ViewCandidates);
                ";

                conn.Execute(
                    insertPermSql,
                    new {
                        UserId = newUserId,
                        JobPostings = request.JobPostings,
                        AccountManagement = request.AccountManagement,
                        ViewCandidates = request.ViewCandidates
                    },
                    transaction: tran
                );

                tran.Commit();

                return new UserWithPermissions
                {
                    UserId = newUserId,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = randomPassword,         // return the random password
                    DateCreated = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    JobPostings = request.JobPostings,
                    AccountManagement = request.AccountManagement,
                    ViewCandidates = request.ViewCandidates
                };
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public UserWithPermissions[] GetAllUsersWithPermissions()
        {
            const string sql = @"
                SELECT 
                    u.UserId,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.PasswordHash,
                    u.DateCreated,
                    u.UpdatedAt,
                    u.LastPasswordResetEmailSentAt,
                    u.LastLogin,
                    COALESCE(p.JobPostings, 0)       AS JobPostings,
                    COALESCE(p.AccountManagement, 0) AS AccountManagement,
                    COALESCE(p.ViewCandidates, 0)    AS ViewCandidates
                FROM Users.AccountData AS u
                LEFT JOIN Users.UserPermissions AS p
                    ON u.UserId = p.UserId
                ORDER BY u.UserId;
            ";

            var results = _dapper.LoadData<UserWithPermissions>(sql);

            // if you prefer an array:
            return results.ToArray();
        }

        public void UpdateLastResetTime(string refId)
        {
            const string sql = @"
                UPDATE Users.AccountData
                SET LastPasswordResetEmailSentAt = SYSUTCDATETIME()
                WHERE UserId = (
                    SELECT UserId 
                    FROM Users.PasswordResetDocs
                    WHERE ReferenceId = @RefId
                );
            ";

            bool updated = _dapper.ExecuteSql(sql, new { RefId = refId });

            if (!updated)
                throw new Exception("Could not update last password reset time — invalid reference ID.");
        }


        public void UpdateUserPassword(string password, int userId)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            const string sql = @"
                UPDATE Users.AccountData
                SET PasswordHash = @PasswordHash,
                    UpdatedAt = SYSUTCDATETIME(),
                    LastPasswordResetEmailSentAt = NULL
                WHERE UserId = @UserId;
            ";

            bool updated = _dapper.ExecuteSql(sql, new
            {
                PasswordHash = hashedPassword,
                UserId = userId
            });

            if (!updated)
            {
                throw new Exception("Could not update user password.");
            }
        }


        public static string GenerateRandomPassword(int length = 10)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable
                .Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray()
            );
        }

    }
}
