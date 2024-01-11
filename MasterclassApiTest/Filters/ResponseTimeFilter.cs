using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MasterclassApiTest.Filters
{
    public class ResponseTimeFilter : IActionFilter
    {
        static private DateTime Before { get; set; }
        static private DateTime After { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            Before = DateTime.Now;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            After = DateTime.Now;
            Debug.WriteLine($"Response time: {(After - Before).TotalMilliseconds}");
        }
    }
}
