using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForgotController : ControllerBase
    {
        private readonly ForgotPasswordService _forgotPasswordService;

        public ForgotController(IConfiguration config)
        {
            _forgotPasswordService = new ForgotPasswordService(config);
        }

        [HttpPost("Send-Reset-Link")]
        public async Task<IActionResult> SendResetLink([FromBody] ResetPasswordRequest req)
        {
            try
            {
                // 👇 IMPORTANT: await this
                var result = await _forgotPasswordService.SendResetLink(req);

                // This now returns a plain bool to the client
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Validate/{referenceId}")]
        public IActionResult ValidateResetLink(string referenceId)
        {

            try
            {
                var result = _forgotPasswordService.isResetDocValid(referenceId);
                return Ok(new { valid = true, referenceId });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            // Do whatever you need with resetId
        }

        [HttpPost("Reset-Password")]
        public IActionResult ResetPassword([FromBody] NewPasswordRequest req)
        {
            try
            {

                _forgotPasswordService.ResetPassword(req);

                return Ok(new {message = "ok"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}