using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using SAConstruction.DTO;
using SAConstruction.Helpers;
using SAConstruction.Repositories;

namespace SAConstruction.Services
{
    public class CreateUserService
    {
        private readonly UserRepository _userRepo;

        public CreateUserService(IConfiguration config)
        {
            _userRepo = new UserRepository(config);
        }

        // helper to get a user by email

        public object CreateUser(CreateUserRequest req)
        {

            // validate request
            if (string.IsNullOrWhiteSpace(req.FirstName) ||
                string.IsNullOrWhiteSpace(req.LastName) ||
                !EmailHelpers.IsValidEmail(req.Email))
            {
                throw new Exception("Request sent to the backend was invalid.");
            }

            // 🔥 use DataContextDapper here to see if the user already exists
            var existingUser = _userRepo.GetUserByEmail(req.Email);




            if (existingUser != null)
            {
                throw new Exception("A user with that email already exists.");
            } 
            

            var createdUser = _userRepo.CreateUser(req);

            return createdUser;


        }
    }
}
