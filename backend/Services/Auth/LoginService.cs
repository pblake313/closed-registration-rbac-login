using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SAConstruction.DTO;
using SAConstruction.Helpers;
using SAConstruction.Models;
using SAConstruction.Repositories;

namespace SAConstruction.Services
{
    public class LoginService
    {
        private readonly UserRepository _userRepo;
        private readonly IConfiguration _config;

        public LoginService(IConfiguration config)
        {
            _config = config;
            _userRepo = new UserRepository(config);
        }

        // 🔥 now returns LoginResponse (access + refresh)
        public LoginResponse Login(LoginRequest req)
        {
            Console.WriteLine(req.Email);
            Console.WriteLine(req.Password);

            // 1) basic validation
            if (!EmailHelpers.IsValidEmail(req.Email) || !PasswordHelpers.IsValidPassword(req.Password))
            {
                throw new Exception("Invalid Email or Password");
            }

            // 2) get a user by email ==> Throw if no user
            var userAccount = _userRepo.GetUserByEmail(req.Email);
            if (userAccount == null)
            {
                throw new Exception("Invalid Email or Password");
            }

            Console.WriteLine(
                $"Found user: {userAccount.UserId} | {userAccount.Email} | {userAccount.FirstName} {userAccount.LastName}"
            );

            // 3) compare passwords with bcrypt
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(req.Password, userAccount.PasswordHash);

            if (!passwordMatches)
            {
                Console.WriteLine($"Invalid password for user {userAccount.UserId}");
                throw new Exception("Wrong Password... still need to handle.");
            }

            // 4) password matches... generate tokens
            var refreshToken = GenerateRefreshToken(userAccount);
            var accessToken = GenerateAccessToken(userAccount);

            var authenticatedUser = _userRepo.GetUserWithPermissionsById(userAccount.UserId);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AuthenticatedUser = authenticatedUser
            };
        }

        private string GenerateAccessToken(User user)
        {
            var secret = _config["jwtAccessSecret"] ?? throw new Exception("Missing jwtAccessSecret");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // short-lived, e.g. 15 minutes
            var expires = DateTime.UtcNow.AddSeconds(45);

            var claims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString()),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken(User user)
        {
            var secret = _config["jwtRefreshSecret"] ?? throw new Exception("Missing jwtRefreshSecret");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // expiration — 21 days
            var expires = DateTime.UtcNow.AddDays(21);

            var claims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString()),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
