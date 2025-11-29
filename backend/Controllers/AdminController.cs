using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;
using SAConstruction.Middleware;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(BasicAuthMiddleware))]   // ✅ make sure we have a valid access token and account.
    [ServiceFilter(typeof(AdminMiddleware))]      // ✅ make sure the user has admin access.


    public class AdminController : ControllerBase
    {
        private readonly CreateUserService _createUserService;
        private readonly GetUsersService _getUsersService;

        public AdminController(IConfiguration config)
        {
            _createUserService = new CreateUserService(config);
            _getUsersService = new GetUsersService(config);
        }

        [HttpPost("Create-User")]
        public IActionResult AddUser([FromBody] CreateUserRequest req)
        {
            try
            {
                var result = _createUserService.CreateUser(req);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Get-Users")]
        public IActionResult GetUsers()
        {
            try
            {
                var result = _getUsersService.GetUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
