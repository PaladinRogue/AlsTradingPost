using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Api.Filters
{
    public class ConcurrencyActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
