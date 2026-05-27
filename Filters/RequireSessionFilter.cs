using CodieGo_Adventure.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodieGo_Adventure.Filters
{
    public class RequireSessionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!(context.Controller is BaseController baseController))
            {
                await next();
                return;
            }

            bool isSessionRestored = await baseController.RestoreSessionFromCookieAsync();
            await baseController.LoadUserHeaderAsync();

            if (!isSessionRestored)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            await next();
        }
    }
}
