using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAConstruction.Models; // for UserWithPermissions

namespace SAConstruction.Middleware
{
    public class CandidateMiddleware : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var http = context.HttpContext;

            // 🔹 Pull user out of BasicAuthMiddleware
            var userObj = http.Items["userRequestingAccess"];

            if (userObj is not UserWithPermissions user)
            {
                Console.WriteLine("⛔ CandidateMiddleware: No authenticated user on context.");

                // 🔥 Force logout just like BasicAuthMiddleware
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Authentication required.",
                    forceLogout = true
                });
                return;
            }

            Console.WriteLine($">>> CandidateMiddleware: User {user.UserId} ({user.Email}) BEFORE AdminController");

            // 🔹 Require admin permission
            if (!user.ViewCandidates)
            {
                Console.WriteLine("⛔ CandidateMiddleware: User is not an admin (ViewCandidates = false).");

                // 🔥 Also force logout if you want to boot them completely
                context.Result = new BadRequestObjectResult(new
                {
                    message = "You do not have permission to access this resource.",
                    forceLogout = true
                });
                return;
            }

            Console.WriteLine("✅ CandidateMiddleware: User has admin permissions.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("<<< CandidateMiddleware: AFTER AdminController");
        }
    }
}
