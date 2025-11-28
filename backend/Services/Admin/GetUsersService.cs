using SAConstruction.Models;
using SAConstruction.Repositories;

namespace SAConstruction.Services
{
    public class GetUsersService
    {
        private readonly UserRepository _userRepo;

        public GetUsersService(IConfiguration config)
        {
            _userRepo = new UserRepository(config);
        }

        public UserWithPermissions[] GetUsers()
        {
            var result = _userRepo.GetAllUsersWithPermissions();
            return result;
        }
    }
}