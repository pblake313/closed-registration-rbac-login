using Microsoft.AspNetCore.Mvc;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataContextDapper _dapper;

        public UserController(IConfiguration config)
        {
            _config = config;
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("Test")]
        public string GetUser()
        {
            string returnableString = "Test";

            return returnableString;
        }

        [HttpPut("edit")]
        public IActionResult EditUser()
        {
            return Ok();
        }

        [HttpPost("add")]
        public IActionResult AddUser(User user)
        {
            string sql = @"
                INSERT INTO Users.AccountData (
                    Email,
                    FirstName,
                    LastName,
                    DateCreated,
                    UpdatedAt
                ) VALUES (
                    @Email,
                    @FirstName,
                    @LastName,
                    SYSUTCDATETIME(),
                    SYSUTCDATETIME()
                )
            ";

            var parameters = new
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok("User created.");
            }

            throw new Exception("Failed to create user.");
        }


    }
}