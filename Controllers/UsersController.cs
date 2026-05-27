using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodieGo_Adventure.Controllers
{
    [ServiceFilter(typeof(RequireSessionFilter))]
    public class UsersController : BaseController
    {
        public UsersController(IGenericServices services) : base(services) { }

        // Endpoint for user profile page
        public async Task<IActionResult> Profile(int id) 
        {
            var checkerId = HttpContext.Session.GetInt32("UserId");
            ViewBag.IsCurrentId = checkerId;

            var data = await _services.GetDataByIdAsync<Profiles>(x => x.ProfileId == id, y => y.Users);

            var modulesProgress = await _services.GetAllDataByIdAsync<ModulesProgress>(x => x.ProfileId == id);
            bool allModulesCompleted = modulesProgress.Any() && modulesProgress.All(x => !x.IsLocked && x.Status == "COMPLETED");
            ViewBag.CertUnlocked = allModulesCompleted;

            data.ModuleProgressCollection = await _services.GetAllDataByIdAsync<ModulesProgress>(x => x.ProfileId == data.ProfileId && x.Status == "COMPLETED");
            data.ChallengeProgressCollection = await _services.GetAllDataByIdAsync<ChallengesProgress>(x => x.ProfileId == data.ProfileId);
            data.LeaderboardCollection = await _services.GetAllDataAsync<Leaderboard>(y => y.ChallengesProgress,
                y => y.ChallengesProgress.Profiles, y => y.ChallengesProgress.Profiles.Users);
            data.PuzzlesCollection = (await _services.GetAllDataAsync<Puzzles>(p => p.ModulesProgress)).Where(p => p.ModulesProgress.ProfileId == id);
            data.AchievementBadgeCollection = (await _services.GetAllDataAsync<AchievementBadge>(a => a.ChallengesProgress)).Where(a => a.ChallengesProgress.ProfileId == id);
            data.HistoryCollection = (await _services.GetAllDataAsync<History>(h => h.ChallengesProgress, h => h.ChallengesProgress.Challenges))
                .Where(a => a.ChallengesProgress.ProfileId == id)
                .OrderByDescending(h => h.logDate)
                .Take(15);

            float bestTime = data.ChallengeProgressCollection.Any() ? data.ChallengeProgressCollection.Max(x => x.Time) : 0;
            TimeSpan time = TimeSpan.FromSeconds(bestTime);

            ViewBag.BestTimeFormatted = $"{time.Minutes:D2}:{time.Seconds:D2}";
            ViewBag.TotalModulesCompleted = data.ModuleProgressCollection.Count();
            ViewBag.TotalChallengePoints = data.ChallengeProgressCollection.Sum(x => x.AccumulatedPoints);

            var rankedList = data.LeaderboardCollection
                .OrderByDescending(x => x.ChallengesProgress.AccumulatedPoints)
                .ThenByDescending(x => x.ChallengesProgress.Time)
                .ToList();

            var rankLevel = rankedList.FindIndex(x => x.ChallengesProgress.Profiles.ProfileId == data.ProfileId);
            ViewBag.RankLevel = rankLevel == -1 ? "Unranked" : $"{rankLevel + 1}";

            return View(data);
        }

        public async Task<IActionResult> UpdateProfileDetail(ProfileDetail dto)
        {
            int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var profile = await _services.GetDataByIdAsync<Profiles>(x => x.ProfileId == Id);

            if (profile != null)
            {
                profile.ConvertToBase64String(dto.ImageFile);
                profile.SetBio(dto.Bio);
                await _services.UpdateDataAsync<Profiles>(profile);
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutUser()
        {
            int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var loginRecords = (await _services.GetAllDataByIdAsync<LoginRecords>(lr => lr.UserId == Id))
                .Where(lr => lr.Status == "Online")
                .OrderByDescending(lr => lr.LoginDateTime)
                .FirstOrDefault();

            if (loginRecords != null)
            {
                loginRecords.SetStatus("Offline");

                await _services.UpdateDataAsync<LoginRecords>(loginRecords);

                // Remove session
                HttpContext.Session.Clear();

                // Remove cookie
                Response.Cookies.Delete("SessionId");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
