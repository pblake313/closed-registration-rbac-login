using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;
using System.Text.Json;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataContextDapper _dapper;

        public AdminController(IConfiguration config)
        {
            _config = config;
            _dapper = new DataContextDapper(config);
        }

        
        private readonly CreateUserService _userService = new();

        [HttpPost("Create-User")]
        public IActionResult AddUser([FromBody] CreateUserRequest req)
        {

            try
            {
                var result = _userService.CreateUser(req);

                Console.WriteLine(result);
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            };

            // // 0) CHECK IF EMAIL ALREADY EXISTS
            // string sqlCheckEmail = @"
            //     SELECT COUNT(*) 
            //     FROM Users.AccountData
            //     WHERE Email = @Email;
            // ";

            // int emailCount = _dapper.LoadDataSingle<int>(sqlCheckEmail, new { Email = req.Email });

            // if (emailCount > 0)
            // {
            //     return StatusCode(400, new
            //     {
            //         errorMessage = "A user with that email already exists."
            //     });
            // }

            // // 1) Insert user, get new UserId + dates
            // string sqlInsertUser = @"
            //     INSERT INTO Users.AccountData (
            //         Email,
            //         FirstName,
            //         LastName,
            //         DateCreated,
            //         UpdatedAt
            //     )
            //     OUTPUT 
            //         INSERTED.UserId,
            //         INSERTED.DateCreated,
            //         INSERTED.UpdatedAt
            //     VALUES (
            //         @Email,
            //         @FirstName,
            //         @LastName,
            //         SYSUTCDATETIME(),
            //         SYSUTCDATETIME()
            //     ); 
            // ";

            // var userParams = new
            // {
            //     Email = req.Email,
            //     FirstName = req.FirstName,
            //     LastName = req.LastName
            // };

            // NewUserRow newUser = _dapper.LoadDataSingle<NewUserRow>(sqlInsertUser, userParams);

            // Console.WriteLine($"New UserId: {newUser.UserId}");

            // // 2) Insert permissions for that user
            // string sqlInsertPermissions = @"
            //     INSERT INTO Users.UserPermissions (
            //         UserId,
            //         JobPostings,
            //         AccountManagement,
            //         ViewCandidates
            //     )
            //     VALUES (
            //         @UserId,
            //         @JobPostings,
            //         @AccountManagement,
            //         @ViewCandidates
            //     );
            // ";

            // var permParams = new
            // {
            //     UserId = newUser.UserId,
            //     JobPostings = req.JobPostings,
            //     AccountManagement = req.AccountManagement,
            //     ViewCandidates = req.ViewCandidates
            // };

            // bool permsInserted = _dapper.ExecuteSql(sqlInsertPermissions, permParams);

            // if (!permsInserted)
            // {
            //     // you could also roll back the user row here if you want to be fancy
            //     return StatusCode(500, new
            //     {
            //         errorMessage = "Failed to save user permissions."
            //     });
            // }

            // // 3) Return something useful to the frontend
            // return Ok(new
            // {
            //     userId = newUser.UserId,
            //     email = req.Email,
            //     firstName = req.FirstName,
            //     lastName = req.LastName,
            //     jobPostings = req.JobPostings,
            //     accountManagement = req.AccountManagement,
            //     viewCandidates = req.ViewCandidates,
            //     dateCreated = newUser.DateCreated,
            //     updatedAt = newUser.UpdatedAt
            // });
        }

        [HttpGet("Get-Users")]
        public IActionResult GetUsers()
        {
            string sql = @"
                SELECT 
                    ad.UserId,
                    ad.Email,
                    ad.FirstName,
                    ad.LastName,
                    ad.DateCreated,
                    ad.UpdatedAt,
                    up.JobPostings,
                    up.AccountManagement,
                    up.ViewCandidates
                FROM Users.AccountData ad
                LEFT JOIN Users.UserPermissions up
                    ON ad.UserId = up.UserId
                ORDER BY ad.DateCreated DESC;
            ";

            // 1) Load as dynamic so we can see exactly what’s coming back
            var rows = _dapper.LoadData<dynamic>(sql).ToList();

            // Console.WriteLine("======= Raw rows from DB =======");
            // Console.WriteLine(JsonSerializer.Serialize(rows, new JsonSerializerOptions
            // {
            //     WriteIndented = true
            // }));

            // 2) Manually map dynamics -> AdminUserDto
            var users = rows.Select(r => new AdminUserDto
            {
                UserId = r.UserId,
                Email = r.Email,
                FirstName = r.FirstName,
                LastName = r.LastName,

                // cast to nullable DateTime
                DateCreated = (DateTime?)r.DateCreated,
                UpdatedAt = (DateTime?)r.UpdatedAt,

                JobPostings = r.JobPostings ?? false,
                AccountManagement = r.AccountManagement ?? false,
                ViewCandidates = r.ViewCandidates ?? false
            }).ToList();

            return Ok(users);
        }
    }
}