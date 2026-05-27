using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodieGo_Adventure.Controllers
{
    [ServiceFilter(typeof(RequireSessionFilter))]
    public class HomeController : BaseController
    {
        public HomeController(IGenericServices services) : base(services) { }

        // Endpoint for dashboard page
        public async Task<IActionResult> Dashboard()
        {
            int id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var data = await _services.GetDataByIdAsync<Profiles>(x => x.ProfileId == id, y => y.Users);

            ViewBag.IsNewUser = data.Users.IsNewUser;

            if (data.Users.IsNewUser)
            {
                if (HttpContext.Session.GetInt32("GuideStep") == null)
                {
                    HttpContext.Session.SetInt32("GuideStep", 0);
                }
            }

            data.ModuleProgressCollection = await _services.GetAllDataByIdAsync<ModulesProgress>(x => x.ProfileId == data.ProfileId && x.Status == "COMPLETED");
            data.ChallengeProgressCollection = await _services.GetAllDataByIdAsync<ChallengesProgress>(x => x.ProfileId == data.ProfileId);
            data.LeaderboardCollection = await _services.GetAllDataAsync<Leaderboard>(y => y.ChallengesProgress,
                y => y.ChallengesProgress.Profiles, y => y.ChallengesProgress.Profiles.Users);
            data.PuzzlesCollection = (await _services.GetAllDataAsync<Puzzles>(p => p.ModulesProgress)).Where(p => p.ModulesProgress.ProfileId == id);
            data.AchievementBadgeCollection = (await _services.GetAllDataAsync<AchievementBadge>(a => a.ChallengesProgress)).Where(a => a.ChallengesProgress.ProfileId == id);

            float bestTime = data.ChallengeProgressCollection.Any() ? data.ChallengeProgressCollection.Max(x => x.Time) : 0;
            TimeSpan time = TimeSpan.FromSeconds(bestTime);

            ViewBag.BestTimeFormatted = $"{time.Minutes:D2}:{time.Seconds:D2}";
            ViewBag.TotalAssessmentScores = data.ChallengeProgressCollection.Sum(c => c.AssessmentScores);
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

        // Endpoint for modules page
        public async Task<IActionResult> Modules()
        {
            int id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var modulesProgress = await _services.GetAllDataByIdAsync<ModulesProgress>(x => x.ProfileId == id, y => y.Modules);

            if (!modulesProgress.Any())
            {
                var modules = await _services.GetAllDataAsync<Modules>();

                int count = 0;
                foreach (var module in modules)
                {
                    var progress = new ModulesProgress { ProfileId = id, ModuleId = module.ModuleId };
                    if (count < 1)
                        progress.UpdateLockStatus(false);
                    else
                        progress.UpdateLockStatus(true);
                    progress.SetCompletedLessons(0);
                    progress.SetStatus("NOT STARTED");

                    await _services.AddDataAsync<ModulesProgress>(progress);
                    count++;
                }

                // reload progress after adding
                modulesProgress = await _services.GetAllDataByIdAsync<ModulesProgress>(x => x.ProfileId == id, y => y.Modules);
            }

            return View(modulesProgress);
        }

        // Endpoint for challenge page
        public async Task<IActionResult> Challenges()
        {
            int id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var modulesProgress = await _services.GetAllDataByIdAsync<ModulesProgress>(x => x.ProfileId == id);
            var challengesProgress = await _services.GetAllDataByIdAsync<ChallengesProgress>(x => x.ProfileId == id, y => y.Challenges);
            bool allModulesCompleted = modulesProgress.Any() && modulesProgress.All(x => !x.IsLocked && x.Status == "COMPLETED");

            DateTime now = DateTime.Now;
            DateTime dailyReset = DateTime.Today.AddHours(7);

            DateTime nextReset = now < dailyReset
                ? dailyReset
                : dailyReset.AddDays(1);
            ViewBag.CountDownTime = (float)(nextReset - now).TotalSeconds;

            var dailiedChallenge = challengesProgress.Where(x => x.IsDaily).OrderBy(x => x.Challenges.Order).FirstOrDefault();

            if (dailiedChallenge != null && now >= dailyReset && (dailiedChallenge.LastDailyReset == null || dailiedChallenge.LastDailyReset < dailyReset))
            {
                dailiedChallenge.SetIsDaily(false);
                await _services.UpdateDataAsync<ChallengesProgress>(dailiedChallenge);
            }

            if (allModulesCompleted)
            {
                var lastLockedChallenge = challengesProgress.Where(x => x.IsLocked).OrderByDescending(x => x.Challenges.Order).FirstOrDefault();

                if (lastLockedChallenge != null)
                {
                    lastLockedChallenge.UpdateLockStatus(false);
                    await _services.UpdateDataAsync<ChallengesProgress>(lastLockedChallenge);
                }
            }

            if (!challengesProgress.Any())
            {
                var challenges = await _services.GetAllDataAsync<Challenges>();

                int count = 0;
                foreach (var challenge in challenges)
                {
                    var progress = new ChallengesProgress { ProfileId = id, ChallengeId = challenge.ChallengeId };
                    progress.SetAccumulatedPoints(0);
                    progress.UpdateLockStatus(true);
                    progress.UpdateTime(0.0f);
                    progress.SetIsDaily(false);
                    progress.SetLastDailyReset(dailyReset);
                    progress.SetCompletedChallenge(0);
                    progress.SetAssessmentScores(0);
                    progress.SetSavedAssessmentScores(10);
                    if (count < 9)
                        progress.SetStatus("NOT STARTED");
                    else 
                        progress.SetStatus("DAILY CHALLENGE");

                    count++;
                    await _services.AddDataAsync<ChallengesProgress>(progress);
                }

                // reload progress after adding
                challengesProgress = await _services.GetAllDataByIdAsync<ChallengesProgress>(x => x.ProfileId == id, y => y.Challenges);
            }

            return View(challengesProgress);
        }

        // Endpoint for leaderboard page
        public async Task<IActionResult> Leaderboard()
        {
            int id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var allLeaderboard = await _services.GetAllDataAsync<Leaderboard>(y => y.ChallengesProgress, 
                y => y.ChallengesProgress.Profiles, y => y.ChallengesProgress.Profiles.Users);
            var usersData = await _services.GetAllDataAsync<Users>(y => y.LoginRecords);

            var rankedList = allLeaderboard
                .OrderByDescending(x => x.ChallengesProgress.AccumulatedPoints)
                .ThenByDescending(x => x.ChallengesProgress.Time)
                .ToList();

            var playersCount = usersData.Count();
            var activePlayers = usersData.Count(x => x.LoginRecords.Any(x => x.Status == "Online"));

            var rankLevel = rankedList.FindIndex(x => x.ChallengesProgress.Profiles.ProfileId == id);
            var top10 = rankedList.Take(10).ToList();

            var leaderboardData = allLeaderboard.FirstOrDefault(l => l.ChallengesProgress.ProfileId == id);

            var userCurrent = usersData.FirstOrDefault(u => u.UserId == id);

            ViewBag.RankLevel = rankLevel == -1 ? "Unranked" : $"{rankLevel + 1}";
            ViewBag.CurrentId = id;
            ViewBag.PlayerCount = playersCount;
            ViewBag.BestTime = leaderboardData != null ? TimeSpan.FromSeconds(leaderboardData.ChallengesProgress.Time).ToString(@"mm\:ss") : "0";
            ViewBag.AccumulatedPoints = leaderboardData != null ? leaderboardData.ChallengesProgress.AccumulatedPoints.ToString("n0") : "0";
            ViewBag.PlayerName = userCurrent != null ? userCurrent.Username : "-";

            return View(top10);
        }

        [HttpPost]
        public async Task<IActionResult> FinishOnboarding()
        {
            int id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var users = await _services.GetDataByIdAsync<Users>(x => x.UserId == id);

            users.SetIsNewUser(false);

            await _services.UpdateDataAsync<Users>(users);

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public IActionResult NextGuideStep()
        {
            int? step = HttpContext.Session.GetInt32("GuideStep");

            if (step != null)
            {
                HttpContext.Session.SetInt32("GuideStep", step.Value + 1);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SkipGuide()
        {
            int id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var user = await _services.GetDataByIdAsync<Users>(x => x.UserId == id);

            if (user != null)
            {
                user.SetIsNewUser(false);
                await _services.UpdateDataAsync<Users>(user);
            }

            // Remove guide session
            HttpContext.Session.Remove("GuideStep");

            var modulesProgress = await _services.GetAllDataByIdAsync<ModulesProgress>(x => x.ProfileId == id);

            if (!modulesProgress.Any())
            {
                var modules = await _services.GetAllDataAsync<Modules>();

                int count = 0;

                foreach (var module in modules)
                {
                    var progress = new ModulesProgress
                    {
                        ProfileId = id,
                        ModuleId = module.ModuleId
                    };

                    if (count < 1)
                        progress.UpdateLockStatus(false);
                    else
                        progress.UpdateLockStatus(true);

                    progress.SetCompletedLessons(0);
                    progress.SetStatus("NOT STARTED");

                    await _services.AddDataAsync<ModulesProgress>(progress);

                    count++;
                }
            }

            var challengesProgress = await _services.GetAllDataByIdAsync<ChallengesProgress>(x => x.ProfileId == id);

            if (!challengesProgress.Any())
            {
                var challenges = await _services.GetAllDataAsync<Challenges>();

                DateTime dailyReset = DateTime.Today.AddHours(7);

                int count = 0;

                foreach (var challenge in challenges)
                {
                    var progress = new ChallengesProgress
                    {
                        ProfileId = id,
                        ChallengeId = challenge.ChallengeId
                    };

                    progress.SetAccumulatedPoints(0);
                    progress.UpdateLockStatus(true);
                    progress.UpdateTime(0.0f);
                    progress.SetIsDaily(false);
                    progress.SetLastDailyReset(dailyReset);
                    progress.SetCompletedChallenge(0);
                    progress.SetAssessmentScores(0);
                    progress.SetSavedAssessmentScores(10);

                    if (count < 9)
                        progress.SetStatus("NOT STARTED");
                    else
                        progress.SetStatus("DAILY CHALLENGE");

                    await _services.AddDataAsync<ChallengesProgress>(progress);

                    count++;
                }
            }

            return Ok();
        }
    }
}
