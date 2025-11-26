using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly CreateUserService _userService;

        public AdminController(IConfiguration config)
        {
            _userService = new CreateUserService(config);
        }

        [HttpPost("Create-User")]
        public IActionResult AddUser([FromBody] CreateUserRequest req)
        {
            try
            {
                var result = _userService.CreateUser(req);
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
            return Ok("TODO: Get more users...");
        }
    }
}
