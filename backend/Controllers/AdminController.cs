using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;
using SAConstruction.Middleware;
using SAConstruction.Repositories;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(BasicAuthMiddleware))]  
    [ServiceFilter(typeof(AdminMiddleware))]    


    public class AdminController : ControllerBase
    {
        private readonly CreateUserService _createUserService;
        private readonly GetUsersService _getUsersService;

        private readonly UserRepository _userRepo;

        public AdminController(IConfiguration config)
        {
            _createUserService = new CreateUserService(config);
            _getUsersService = new GetUsersService(config);
            _userRepo = new UserRepository(config);
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

        [HttpGet("Get-User/{userId:int}")]
        public IActionResult GetSingleUser(int userId)
        {
            try
            {
                Console.WriteLine("Test ew branc ruleset.");

                var result = _userRepo.GetUserWithPermissionsById(userId);

                if (result == null)
                {
                    throw new Exception("User not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("Delete-User/{userId:int}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                _userRepo.DeleteUser(userId);
                return Ok("User Deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("Edit-User/{userId:int}")]
        public IActionResult EditUser(int userId, [FromBody] EditUserRequest req)
        {
            try
            {   
                _userRepo.UpdateUser(userId, req);

                var updatedUser = _userRepo.GetUserWithPermissionsById(userId);
                return Ok(new {updatedUser});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
