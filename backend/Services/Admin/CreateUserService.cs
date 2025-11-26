using System.Text.Json;
using SAConstruction.DTO;
using SAConstruction.Helpers;

namespace SAConstruction.Services
{
    public class CreateUserService
    {

        
        public object CreateUser(CreateUserRequest req)
        {


            Console.WriteLine("======= Incoming CreateUserRequest =======");


            Console.WriteLine(JsonSerializer.Serialize(req, new JsonSerializerOptions
            {
                WriteIndented = true
            }));



            // validate request
            if (string.IsNullOrWhiteSpace(req.FirstName) || string.IsNullOrWhiteSpace(req.LastName) || !EmailHelpers.IsValidEmail(req.Email))
            {
                throw new Exception("Request sent to the backend was invalid.");
            }


            // help me call a function to return a from my database here....

            throw new Exception("ok 4 now");

        }
    }
}