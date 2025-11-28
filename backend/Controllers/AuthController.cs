using Microsoft.AspNetCore.Mvc;
using SAConstruction.DTO;
using SAConstruction.Services;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        
        private readonly LoginService _loginService;


        public AuthController(IConfiguration config)
        {
            _loginService = new LoginService(config);
        }

        [HttpPost("Login")]
      public IActionResult Login([FromBody] LoginRequest req)
        {
            try
            {
                var successfulResponse = _loginService.Login(req);

                Response.Cookies.Append("refreshToken", successfulResponse.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(21)
                });

                return Ok(new
                {
                    accessToken = successfulResponse.AccessToken,
                    authenticatedUser = successfulResponse.AuthenticatedUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}