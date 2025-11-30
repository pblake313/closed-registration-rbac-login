using Microsoft.AspNetCore.Mvc;
using SAConstruction.Middleware;
using SAConstruction.Models;
using SAConstruction.Repositories;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(BasicAuthMiddleware))]



    public class AccountController : ControllerBase
    {

        private readonly UserRepository _userRepo;

        public AccountController(IConfiguration config)
        {
            _userRepo = new UserRepository(config);
            
        }

        [HttpGet("Get-Authenticated-User")]
        public IActionResult GetAuthenticatedUser()
        {
            try
            {
                // 🔹 pulled from middleware
                var user = HttpContext.Items["userRequestingAccess"] as UserWithPermissions;

                if (user == null)
                {
                    // middleware didn’t set it (invalid/expired token, etc.)
                    return Unauthorized(new { message = "User context not found" });
                }


                var authenticatedUser = _userRepo.UpdateUserAutoLoginTime(user.UserId);

                // you can shape this however you want
                return Ok(new
                {
                    authenticatedUser.UserId,
                    authenticatedUser.Email,
                    authenticatedUser.FirstName,
                    authenticatedUser.LastName,
                    authenticatedUser.DateCreated,
                    authenticatedUser.UpdatedAt,
                    authenticatedUser.LastPasswordResetEmailSentAt,
                    authenticatedUser.JobPostings,
                    authenticatedUser.AccountManagement,
                    authenticatedUser.ViewCandidates,
                    authenticatedUser.LastAutoLogin,
                    authenticatedUser.LastLogin
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        } 
    }
}
