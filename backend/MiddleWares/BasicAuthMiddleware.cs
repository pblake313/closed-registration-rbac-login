using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SAConstruction.Repositories;
using SAConstruction.Services;

namespace SAConstruction.Middleware
{
    public class BasicAuthMiddleware : IActionFilter
    {

        private readonly IConfiguration _config;
        private readonly UserRepository _userRepo;
        private readonly TokenService _tokenService;

        public BasicAuthMiddleware(IConfiguration config)
        {
            _config = config;
            _userRepo = new UserRepository(config);
            _tokenService = new TokenService(config);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var http = context.HttpContext;

            // ===== ACCESS TOKEN =====
            var authHeader = http.Request.Headers["Authorization"].FirstOrDefault();
            string? accessToken = null;

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                accessToken = authHeader["Bearer ".Length..].Trim();
            }

            // ===== REFRESH TOKEN =====
            var refreshToken = http.Request.Cookies["refreshToken"];

            // if we have an accessToken
            Console.WriteLine($"🔵 Original Access Token: {accessToken ?? "NULL"}");
            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = _config["jwtAccessSecret"] ?? throw new Exception("Missing jwtAccessSecret");
                var key = Encoding.UTF8.GetBytes(secret);

                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(accessToken, validationParams, out var validatedToken);


                // get the userId from the token payload.
                var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "userId");

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId) || userId <= 0)
                {
                    throw new SecurityTokenException("Access token missing or invalid userId claim.");
                }

                var userToSendDown = _userRepo.GetUserWithPermissionsById(userId);

                Console.WriteLine("🟢 Original token still valid! Can Access Resources");

                http.Items["userRequestingAccess"] = userToSendDown;

                // try to verify the access token
            }
            catch (SecurityTokenExpiredException ex)
            {
                Console.WriteLine("🟡 Access token EXPIRED:");
                Console.WriteLine(ex.Message);
                // attempt to verify my refresh token...

                try
                {
                    Console.WriteLine("🟣 Attempting Refresh.");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var refreshSecret = _config["jwtRefreshSecret"] ?? throw new Exception("Missing jwtRefreshSecret");
                    var refreshKey = Encoding.UTF8.GetBytes(refreshSecret);

                    var refreshValidationParams = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(refreshKey),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,    // 🔥 refresh token also has exp
                        ClockSkew = TimeSpan.Zero
                    };

                    var refreshPrincipal = tokenHandler.ValidateToken(refreshToken, refreshValidationParams, out var refreshValidatedToken);

                    // here check for the claim type to be userId... it should have a number... throw an error if not...
                    var userIdClaim = refreshPrincipal.Claims.FirstOrDefault(c => c.Type == "userId");

                    if (userIdClaim == null)
                    {
                        throw new SecurityTokenException("Refresh token missing userId claim.");
                    }

                    if (!int.TryParse(userIdClaim.Value, out var userId) || userId <= 0)
                    {
                        throw new SecurityTokenException("Refresh token userId claim is invalid.");
                    }

                    // now fetch the user properly using int userId
                    var userRequesting = _userRepo.GetUserById(userId);

                    if (userRequesting == null)
                    {
                        throw new SecurityTokenException("User does not exist for refresh token.");
                    }

                    // generate a token for the userRequesting
                    var newAccessToken = _tokenService.GenerateAccessToken(userRequesting);

                    var userToSendDown = _userRepo.GetUserWithPermissionsById(userId);

                    Console.WriteLine("🟢 New Access Token Created... Can Access Resources.");

                    // return the new access token in the headers...
                    http.Response.Headers["x-new-access-token"] = newAccessToken;

                    http.Items["userRequestingAccess"] = userToSendDown;

                }
                catch (Exception refreshExecption)
                {
                    Console.WriteLine("⛔ Invalid or expired access token.");
                    context.Result = new BadRequestObjectResult(new
                    {
                        message = refreshExecption.Message,
                        forceLogout = true
                    });
                }
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine("⛔ Access token INVALID:");
                // do not attempt to issue a new token with the refresh token, since we couldnt confirm the secret password.
                context.Result = new BadRequestObjectResult(new
                {
                    message = ex.Message,
                    forceLogout = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("⛔ Unknown error:");
                // Console.WriteLine(ex.Message);
                context.Result = new BadRequestObjectResult(new
                {
                    message = ex.Message,
                    forceLogout = true
                });
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("<<< BasicAuthMiddleware: AFTER controller");
        }
    }
}
