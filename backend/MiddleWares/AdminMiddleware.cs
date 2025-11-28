using Microsoft.AspNetCore.Mvc.Filters;

namespace SAConstruction.Middleware
{
    public class AdminMiddleware : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(">>> AdminMiddleware: BEFORE AdminController");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("<<< AdminMiddleware: AFTER AdminController");
        }
    }
}
