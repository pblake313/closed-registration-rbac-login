using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace SAConstruction.Middleware
{
    public class BasicAuthMiddleware : IActionFilter
    {
            
            private readonly IConfiguration _config;

            public BasicAuthMiddleware(IConfiguration config)
            {
                _config = config;
            }

        
         public void OnActionExecuting(ActionExecutingContext context)
        {
            var http = context.HttpContext;

            // ===== ACCESS TOKEN =====
            var authHeader = http.Request.Headers["Authorization"].FirstOrDefault();
            string? accessToken = null;

            if (!string.IsNullOrEmpty(authHeader) &&
                authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                accessToken = authHeader["Bearer ".Length..].Trim();
            }

            // ===== REFRESH TOKEN =====
            var refreshToken = http.Request.Cookies["refreshToken"];

            Console.WriteLine(">>> BasicAuthMiddleware: BEFORE controller");
            Console.WriteLine($"Access Token: {accessToken ?? "NULL"}");
            Console.WriteLine($"Refresh Token: {refreshToken ?? "NULL"}");


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

                Console.WriteLine("✅ Token is VALID");

                Console.WriteLine("------ PAYLOAD (CLAIMS) ------");
                foreach (var claim in principal.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
                Console.WriteLine("-------------------------------");

                // try to verify the access token
            } 
            catch (Exception verifyAccessTokenError)
            {
                //access token is not valid.

            }


            http.Items["AccessToken"] = accessToken;
            http.Items["RefreshToken"] = refreshToken;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("<<< BasicAuthMiddleware: AFTER controller");
        }
    }
}
