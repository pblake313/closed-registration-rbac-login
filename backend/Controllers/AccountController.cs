using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;
using SAConstruction.Middleware;
using SAConstruction.Models; 

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(BasicAuthMiddleware))]



    public class AccountController : ControllerBase
    {
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

                // you can shape this however you want
                return Ok(new
                {
                    user.UserId,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.DateCreated,
                    user.UpdatedAt,
                    user.LastPasswordResetEmailSentAt,
                    user.JobPostings,
                    user.AccountManagement,
                    user.ViewCandidates,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
