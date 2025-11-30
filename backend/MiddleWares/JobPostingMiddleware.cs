using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAConstruction.Models; // for UserWithPermissions

namespace SAConstruction.Middleware
{
    public class JobPostingMiddleware : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var http = context.HttpContext;

            var userObj = http.Items["userRequestingAccess"];

            if (userObj is not UserWithPermissions user)
            {

                context.Result = new BadRequestObjectResult(new
                {
                    message = "Authentication required.",
                    forceLogout = true
                });
                return;
            }

            if (!user.JobPostings)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "You do not have permission to access this resource.",
                    forceLogout = true
                });
                return;
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Console.WriteLine("<<< JobPostingMiddleware: AFTER AdminController");
        }
    }
}
