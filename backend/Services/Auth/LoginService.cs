using SAConstruction.DTO;
using SAConstruction.Helpers;
using SAConstruction.Repositories;
using SAConstruction.Services; // make sure you import this

public class LoginService
{
    private readonly UserRepository _userRepo;
    private readonly IConfiguration _config;
    private readonly TokenService _tokenService;

    public LoginService(IConfiguration config)
    {
        _config = config;
        _userRepo = new UserRepository(config);
        _tokenService = new TokenService(config);
    }

    public LoginResponse Login(LoginRequest req)
    {
        if (!EmailHelpers.IsValidEmail(req.Email) || !PasswordHelpers.IsValidPassword(req.Password))
        {
            throw new Exception("Invalid Email or Password");
        }

        var userAccount = _userRepo.GetUserByEmail(req.Email);
        if (userAccount == null)
        {
            throw new Exception("Invalid Email or Password");
        }

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(req.Password, userAccount.PasswordHash);

        if (!passwordMatches)
        {
            Console.WriteLine($"Invalid password for user {userAccount.UserId}");
            throw new Exception("Wrong Password... still need to handle.");
        }

        // 🔥 use TokenService instead of local methods
        var (accessToken, refreshToken) = _tokenService.GenerateTokenPair(userAccount);

        var authenticatedUser = _userRepo.GetUserWithPermissionsById(userAccount.UserId);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AuthenticatedUser = authenticatedUser
        };
    }
}
