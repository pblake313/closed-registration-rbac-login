using SAConstruction.DTO;
using SAConstruction.Helpers;

namespace SAConstruction.Services
{
    public class CreateUserService
    {
        private readonly DataContextDapper _dapper;

        public CreateUserService(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        // helper to get a user by email
        public UserDto? GetUserByEmail(string email)
        {
            const string sql = @"
                SELECT TOP 1 
                    FirstName,
                    LastName,
                    Email
                FROM Users.AccountData
                WHERE Email = @Email;
            ";

            // LoadData returns IEnumerable<T>, so just grab FirstOrDefault
            var users = _dapper.LoadData<UserDto>(sql, new { Email = email });

            return users.FirstOrDefault();
        }

        public object CreateUser(CreateUserRequest req)
        {

            // validate request
            if (string.IsNullOrWhiteSpace(req.FirstName) ||
                string.IsNullOrWhiteSpace(req.LastName) ||
                !EmailHelpers.IsValidEmail(req.Email))
            {
                throw new Exception("Request sent to the backend was invalid.");
            }

            // 🔥 use DataContextDapper here to see if the user already exists
            var existingUser = GetUserByEmail(req.Email);

            if (existingUser != null)
            {
                throw new Exception("A user with that email already exists.");
            } else
            {
                throw new Exception("No user yet...");
                
            }



        }
    }
}
