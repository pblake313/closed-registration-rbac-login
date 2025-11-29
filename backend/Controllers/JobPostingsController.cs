using Microsoft.AspNetCore.Mvc;
using SAConstruction.Middleware;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(BasicAuthMiddleware))]  
    [ServiceFilter(typeof(JobPostingMiddleware))]      

    public class JobPostingsController : ControllerBase
    {

        [HttpGet("Test")]
        public IActionResult GetUsers()
        {
            try
            {
                return Ok(new { message = "You can access the Job Postings routes! Your account has JobPostings access!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
