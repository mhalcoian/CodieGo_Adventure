using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Game.Challenges;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CodieGo_Adventure.Controllers
{
    [ServiceFilter(typeof(RequireSessionFilter))]
    public class ExperienceController : BaseController
    {
        public ExperienceController(IGenericServices services) : base(services) { }

        public async Task<IActionResult> Module(int order)
        {
            int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var moduleProgress = await _services.GetDataByIdAsync<ModulesProgress>(x => x.ProfileId == Id && x.Modules.Order == order, y => y.Modules);

            ViewBag.Order = order;

            ViewBag.LessonKey = moduleProgress.Modules.Title
                .Trim()
                .ToLower()
                .Replace(" ", "-");

            ViewBag.TotalStages = moduleProgress.Modules.TotalLessons;

            if (moduleProgress != null && moduleProgress.IsLocked)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "warning",
                    title = "WARNING",
                    message = "You big brain piece of shit"
                });

                return RedirectToAction("Modules", "Home");
            }

            if (moduleProgress != null && moduleProgress.Status == "NOT STARTED")
            {
                moduleProgress.SetStatus("IN PROGRESS");
                await _services.UpdateDataAsync<ModulesProgress>(moduleProgress);
            }

            if (moduleProgress == null) return null;

            ViewBag.CurrentProgress = moduleProgress.CompletedLessons;

            if (moduleProgress != null && moduleProgress.Status == "COMPLETED")
                ViewBag.CurrentProgress = 0;

            return View();
        }

        public async Task<IActionResult> Challenge(int order)
        {
            int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

            ViewBag.ChallengeOrder = order;

            var challengeProgress = await _services.GetDataByIdAsync<ChallengesProgress>(x => x.ProfileId == Id && x.Challenges.Order == order, y => y.Challenges);

            ViewBag.TotalTask = challengeProgress.Challenges.TotalChallenge;

            if (challengeProgress == null)
                return RedirectToAction("Challenges", "Home");

            if (challengeProgress.IsLocked)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "warning",
                    title = "WARNING",
                    message = "You big brain piece of shit"
                });

                return RedirectToAction("Challenges", "Home");
            }

            if (challengeProgress.Status != "DAILY CHALLENGE")
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "warning",
                    title = "WARNING",
                    message = "What are you even tryna do?"
                });

                return RedirectToAction("Challenges", "Home");
            }

            var leaderboard = await _services.GetDataByIdAsync<Leaderboard>(x => x.LeaderboardId == challengeProgress.CProgressId && x.ChallengesProgress.Profiles.ProfileId == Id && x.ChallengesProgress.Challenges.Order == order, y => y.ChallengesProgress, y => y.ChallengesProgress.Challenges);

            if (leaderboard == null)
            {
                leaderboard = new Leaderboard();
                leaderboard.SetLeaderboardId(challengeProgress.CProgressId);
                leaderboard.SetTotalPoints(0);
                leaderboard.SetBestTime(0);

                await _services.AddDataAsync<Leaderboard>(leaderboard);
            }

            var history = await _services.GetDataByIdAsync<History>(h => h.HistoryId == challengeProgress.CProgressId && h.ChallengesProgress.ProfileId == Id && h.logDate.Date == DateTime.Now.Date, h => h.ChallengesProgress, h => h.ChallengesProgress.Profiles);

            if (history == null)
            {
                History newHistory = new History();
                newHistory.SetHistoryId(challengeProgress.CProgressId);
                newHistory.SetDailyPoints(0);
                newHistory.SetLogDate();
                await _services.AddDataAsync<History>(newHistory);
            }

            DateTime now = DateTime.Now;
            DateTime dailyReset = DateTime.Today.AddHours(7);
            bool canPlayToday = !challengeProgress.IsLocked && !challengeProgress.IsDaily && (challengeProgress.LastDailyReset == null || challengeProgress.LastDailyReset < dailyReset);

            if (!canPlayToday)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "You have already completed today's daily challenge!"
                });

                return RedirectToAction("Challenges", "Home");
            }

            if (challengeProgress != null)
            {
                float timeStart = challengeProgress.Challenges.TimeLimit;
                int totalChallenges = ChallengePool.All.Count;
                int timePerChallenge = (int)timeStart / totalChallenges;
                TimeSpan time = TimeSpan.FromSeconds(timeStart);
                ViewBag.TimeFormatted = $"{time.Minutes:D2}:{time.Seconds:D2}";
                ViewBag.TimeSeconds = (int)timeStart;
                ViewBag.TimePerChallenge = timePerChallenge;

                challengeProgress.SetIsDaily(true);
                challengeProgress.SetLastDailyReset(dailyReset);
                await _services.UpdateDataAsync<ChallengesProgress>(challengeProgress);
            }

            return View();
        }

        public async Task<IActionResult> Assessment(int order)
        {
            int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var challengeProgress = await _services.GetDataByIdAsync<ChallengesProgress>(x => x.ProfileId == Id && x.Challenges.Order == order, y => y.Challenges);

            ViewBag.Order = order;

            ViewBag.LessonKey = challengeProgress.Challenges.Title
                .Trim()
                .ToLower()
                .Replace(" ", "-");

            ViewBag.TotalStages = challengeProgress.Challenges.TotalChallenge;

            if (challengeProgress != null && challengeProgress.IsLocked)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "warning",
                    title = "WARNING",
                    message = "You big brain piece of shit"
                });

                return RedirectToAction("Challenges", "Home");
            }

            if (challengeProgress != null && challengeProgress.Status == "DAILY CHALLENGE")
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "warning",
                    title = "WARNING",
                    message = "What are you even tryna do?"
                });

                return RedirectToAction("Challenges", "Home");
            }

            if (challengeProgress != null && challengeProgress.Status == "NOT STARTED")
            {
                challengeProgress.SetStatus("IN PROGRESS");
                await _services.UpdateDataAsync<ChallengesProgress>(challengeProgress);
            }

            if (challengeProgress == null) return null;

            ViewBag.CurrentProgress = challengeProgress.CompletedChallenge;

            if (challengeProgress != null && challengeProgress.Status == "COMPLETED")
                ViewBag.CurrentProgress = 0;

            return View();
        }
    }
}
