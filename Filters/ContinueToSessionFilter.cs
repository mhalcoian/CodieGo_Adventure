using CodieGo_Adventure.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodieGo_Adventure.Filters
{
    public class ContinueToSessionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!(context.Controller is BaseController baseController))
            {
                await next();
                return;
            }

            await baseController.RestoreSessionFromCookieAsync();
            await baseController.LoadUserHeaderAsync();

            await next();
        }
    }
}
