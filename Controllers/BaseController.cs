using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodieGo_Adventure.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IGenericServices _services;

        protected BaseController(IGenericServices services)
        {
            _services = services;
        }

        protected internal async Task<bool> RestoreSessionFromCookieAsync()
        {
            // Already logged in
            if (HttpContext.Session.GetInt32("UserId") != null)
                return true;

            // No cookie, not logged in
            if (!Request.Cookies.TryGetValue("SessionId", out string tokenValue))
                return false;

            if (!Guid.TryParse(tokenValue, out Guid token))
                return false;

            var record = await _services.GetDataBySessionIdAsync<LoginRecords>(token);

            if (record == null)
                return false;

            // Restore session
            HttpContext.Session.SetInt32("UserId", record.UserId);
            return true;
        }

        protected internal async Task LoadUserHeaderAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return;

            int id = userId.Value;

            var user = await _services.GetDataByIdAsync<Users>(x => x.UserId == id);

            ViewData["UserHeader"] = user;
        }
    }
}
