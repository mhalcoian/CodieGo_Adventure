using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CodieGo_Adventure.Controllers
{
    [ServiceFilter(typeof(ContinueToSessionFilter))]
    public class LandingController : BaseController
    {
        public LandingController(IGenericServices services) : base(services) { }

        // Endpoint for page
        public async Task<IActionResult> Page()
        {
            var allPlayers = await _services.GetAllDataAsync<Users>(y => y.LoginRecords);
            var allChallenges = await _services.GetAllDataAsync<Challenges>();
            var totalPlayers = allPlayers.Count();
            var challenges = allChallenges.Count();
            bool hasProgress = HttpContext.Session.GetInt32("UserId") != null;

            if (hasProgress)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "success",
                    title = "SUCCESS",
                    message = "Welcome back, Player."
                });
            }

            ViewBag.HasSession = hasProgress;
            ViewBag.TotalPlayers = totalPlayers;
            ViewBag.Challenges = challenges;

            return View();
        }
    }
}
