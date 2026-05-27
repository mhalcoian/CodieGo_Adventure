using CodieGo_Adventure.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodieGo_Adventure.Filters
{
    public class RedirectIfAuthenticatedFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!(context.Controller is BaseController baseController))
            {
                await next();
                return;
            }

            bool isSessionRestored = await baseController.RestoreSessionFromCookieAsync();

            if (isSessionRestored)
            {
                context.Result = new RedirectToActionResult("Dashboard", "Home", null);
                return;
            }

            await next();
        }
    }
}
