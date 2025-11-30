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
        var userAccount = _userRepo.GetUserByEmail(req.Email);
        if (userAccount == null || req.Password == null)
        {
            throw new Exception("Invalid Email or Password");
        }

        // here check if the account is locked... 
        if (userAccount.AccountLockedUntil.HasValue && userAccount.AccountLockedUntil > DateTime.UtcNow)
        {
            // You can customize this message however you want
            var lockedUntilLocal = userAccount.AccountLockedUntil.Value.ToLocalTime();
            throw new Exception($"Your account is locked until {lockedUntilLocal:g}. Please try again later.");
        }

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(req.Password, userAccount.PasswordHash);

        if (!passwordMatches)
        {
            Console.WriteLine($"Invalid password for user {userAccount.UserId}");

            _userRepo.IncrementLoginAttemptsAndLock(userAccount.UserId);

            throw new Exception("Invalid Email or Password");
        }

        // 🔥 use TokenService instead of local methods
        var (accessToken, refreshToken) = _tokenService.GenerateTokenPair(userAccount);

        // here update the users LastLoginTime
        var authenticatedUser = _userRepo.UpdateUserLoginTime(userAccount.UserId);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AuthenticatedUser = authenticatedUser
        };
    }
}
