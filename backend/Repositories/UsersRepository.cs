using System.Data;
using Microsoft.Data.SqlClient;
using Dapper; 
using SAConstruction.Models;
using SAConstruction.DTO;

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
                    LastLogin,
                    LastAutoLogin,
                    PasswordAttempts,
                    AccountLockedUntil
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
                    LastLogin,
                    LastAutoLogin,
                    PasswordAttempts,
                    AccountLockedUntil

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
                    u.LastLogin,
                    u.LastAutoLogin,
                    u.PasswordAttempts,
                    u.AccountLockedUntil,
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
                throw new Exception($"User not found.");

            return user;
        }

        public UserWithPermissions CreateUser(CreateUserRequest request)
        {
            // 1) Generate a random password (plain)
            string randomPassword = GenerateRandomPassword(10);

            // 2) Hash it BEFORE storing in the database
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(randomPassword);

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                // ---- INSERT ACCOUNT ----
                const string insertAccountSql = @"
                    INSERT INTO Users.AccountData
                        (Email, FirstName, LastName, PasswordHash, DateCreated, UpdatedAt)
                    VALUES
                        (@Email, @FirstName, @LastName, @PasswordHash, @Now, @Now);
                    SELECT CAST(SCOPE_IDENTITY() AS int);
                ";

                var newUserId = conn.QuerySingle<int>(
                    insertAccountSql,
                    new
                    {
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PasswordHash = hashedPassword,    // ✅ store HASHED password
                        Now = DateTime.UtcNow
                    },
                    transaction: tran
                );

                // ---- INSERT PERMISSIONS ----
                const string insertPermSql = @"
                    INSERT INTO Users.UserPermissions
                        (UserId, JobPostings, AccountManagement, ViewCandidates)
                    VALUES
                        (@UserId, @JobPostings, @AccountManagement, @ViewCandidates);
                ";

                conn.Execute(
                    insertPermSql,
                    new
                    {
                        UserId = newUserId,
                        JobPostings = request.JobPostings,
                        AccountManagement = request.AccountManagement,
                        ViewCandidates = request.ViewCandidates
                    },
                    transaction: tran
                );

                tran.Commit();

                // 3) Return user WITH the plain random password (so admin can send it)
                return new UserWithPermissions
                {
                    UserId = newUserId,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = randomPassword,     // ✅ return PLAIN password
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
                    u.LastAutoLogin,
                    u.PasswordAttempts,
                    u.AccountLockedUntil,
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

        public UserWithPermissions UpdateUserLoginTime(int userId)
        {
            const string sql = @"
                UPDATE Users.AccountData
                SET 
                    LastLogin = SYSUTCDATETIME(),
                    UpdatedAt = SYSUTCDATETIME(),
                    PasswordAttempts = 0,
                    AccountLockedUntil = NULL
                WHERE UserId = @UserId;
            ";

            bool updated = _dapper.ExecuteSql(sql, new { UserId = userId });

            if (!updated)
            {
                throw new Exception($"Could not update last login time for user with id {userId}.");
            }

            // Return the current user data (including updated LastLogin)
            return GetUserWithPermissionsById(userId);
        }

        public UserWithPermissions UpdateUserAutoLoginTime(int userId)
        {
            const string sql = @"
                UPDATE Users.AccountData
                SET 
                    LastAutoLogin = SYSUTCDATETIME(),
                    UpdatedAt = SYSUTCDATETIME()
                WHERE UserId = @UserId;
            ";

            bool updated = _dapper.ExecuteSql(sql, new { UserId = userId });

            if (!updated)
            {
                throw new Exception($"Could not update last login time for user with id {userId}.");
            }

            // Return the current user data (including updated LastLogin)
            return GetUserWithPermissionsById(userId);
        }


        public int IncrementLoginAttemptsAndLock(int userId)
        {

            const string sql = @"
                UPDATE Users.AccountData
                SET 
                    PasswordAttempts = CASE 
                        -- If this attempt triggers lockout
                        WHEN ISNULL(PasswordAttempts, 0) + 1 >= 7
                            THEN 0                              -- reset attempts
                        ELSE ISNULL(PasswordAttempts, 0) + 1   -- increment normally
                    END,
                    UpdatedAt = SYSUTCDATETIME(),
                    
                    AccountLockedUntil = CASE
                        -- If this attempt triggers lockout and not already locked
                        WHEN ISNULL(PasswordAttempts, 0) + 1 >= 7
                            AND (AccountLockedUntil IS NULL OR AccountLockedUntil < SYSUTCDATETIME())
                            THEN DATEADD(hour, 1, SYSUTCDATETIME())
                        ELSE AccountLockedUntil
                    END

                OUTPUT INSERTED.PasswordAttempts
                WHERE UserId = @UserId;
            ";

            var attempts = _dapper.LoadDataSingle<int>(sql, new { UserId = userId });

            if (attempts < 0)
                throw new Exception("An error on our end has occurred. Please try again later.");

            return attempts;
        }

        public void UpdateUser(int userId, EditUserRequest req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req), "EditUserRequest cannot be null.");
            }

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                // 1) Update basic account data
                const string updateAccountSql = @"
                    UPDATE Users.AccountData
                    SET 
                        FirstName = @FirstName,
                        LastName  = @LastName,
                        UpdatedAt = SYSUTCDATETIME()
                    WHERE UserId = @UserId;
                ";

                int affectedAccountRows = conn.Execute(
                    updateAccountSql,
                    new
                    {
                        UserId = userId,
                        FirstName = req.FirstName,
                        LastName = req.LastName
                    },
                    transaction: tran
                );

                if (affectedAccountRows == 0)
                {
                    throw new Exception($"User with id {userId} not found in AccountData.");
                }

                // 2) Update permissions
                const string updatePermsSql = @"
                    UPDATE Users.UserPermissions
                    SET
                        JobPostings       = @JobPostings,
                        AccountManagement = @AccountManagement,
                        ViewCandidates    = @ViewCandidates
                    WHERE UserId = @UserId;
                ";

                int affectedPermRows = conn.Execute(
                    updatePermsSql,
                    new
                    {
                        UserId = userId,
                        JobPostings = req.JobPostings,
                        AccountManagement = req.AccountManagement,
                        ViewCandidates = req.ViewCandidates
                    },
                    transaction: tran
                );

                if (affectedPermRows == 0)
                {
                    throw new Exception($"Permissions for user id {userId} not found.");
                }

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public void DeleteUser(int userId)
        {
            const string sql = @"
                DELETE FROM Users.AccountData
                WHERE UserId = @UserId;
            ";

            var deleted = _dapper.ExecuteSql(sql, new { UserId = userId });

            if (!deleted)
            {
                throw new Exception($"User with id {userId} not found or could not be deleted.");
            }
        }

        public void UpdateLastResetTime(string refId)
        {
            const string sql = @"
                UPDATE Users.AccountData
                SET 
                    LastPasswordResetEmailSentAt = SYSUTCDATETIME(),
                    UpdatedAt = SYSUTCDATETIME()
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
