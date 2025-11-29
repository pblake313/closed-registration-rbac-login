using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SAConstruction.Models;

namespace SAConstruction.Services
{
    public class TokenService
    {
        private readonly string _accessSecret;
        private readonly string _refreshSecret;

        public TokenService(IConfiguration config)
        {
            _accessSecret = config["jwtAccessSecret"] 
                ?? throw new Exception("Missing jwtAccessSecret");
            _refreshSecret = config["jwtRefreshSecret"] 
                ?? throw new Exception("Missing jwtRefreshSecret");
        }

        public string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddSeconds(10); // or whatever time you want

            var claims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(21);

            var claims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (string AccessToken, string RefreshToken) GenerateTokenPair(User user)
        {
            var access = GenerateAccessToken(user);
            var refresh = GenerateRefreshToken(user);
            return (access, refresh);
        }
    }
}
