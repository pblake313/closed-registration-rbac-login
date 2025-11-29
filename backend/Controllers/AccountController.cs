using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;
using SAConstruction.Middleware;

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
                return Ok(new {message = "Ok 4 now."});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
