using SAConstruction.Models;

namespace SAConstruction.DTO
{
    public class LoginResponse
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string AccessToken {get; set;} = string.Empty;

        public UserWithPermissions? AuthenticatedUser {get; set;}
    }

}