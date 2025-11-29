using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;
using SAConstruction.Middleware;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(BasicAuthMiddleware))]   // ✅ make sure we have a valid access token and account.
    [ServiceFilter(typeof(CandidateMiddleware))]      // ✅ make sure the user has admin access.


    public class CandidatesController : ControllerBase
    {

        [HttpGet("Test")]
        public IActionResult GetUsers()
        {
            try
            {
                return Ok(new { message = "You can access the candidates routes! Your account has ViewCadidates access!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
