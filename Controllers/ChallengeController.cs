using CodieGo_Adventure.Controllers;
using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Game.Challenges;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

[ApiController]
[Route("[controller]")]
[ServiceFilter(typeof(RequireSessionFilter))]
public class ChallengeController : BaseController
{
    public ChallengeController(IGenericServices services) : base(services) { }

    // initializer
    [HttpGet("Init")]
    public IActionResult Init()
    {
        GameSessionStore.Reset();

        var session = GameSessionStore.Get();

        return Json(new
        {
            challenge = session.Current == null ? null : session.Current.Title + "\n\n" + session.Current.Guide,

            skips = session.numberOfSkips
        });
    }

    [HttpPost("Compile")]
    public IActionResult Compile([FromBody] string code)
    {
        var session = GameSessionStore.Get();

        if (session.Current == null || session.GameEnded)
        {
            return Json(new
            {
                finished = true,
                message = "Game already finished."
            });
        }

        var challenge = session.Current;

        // compile
        var outputWriter = new StringWriter();
        Console.SetOut(outputWriter);

        var tree = CSharpSyntaxTree.ParseText(code);

        var refs = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .ToList();

        var compilation = CSharpCompilation.Create(
            "UserCode",
            new[] { tree },
            refs,
            new CSharpCompilationOptions(OutputKind.ConsoleApplication)
        );

        using var ms = new MemoryStream();
        var emit = compilation.Emit(ms);

        if (!emit.Success)
        {
            var errors = emit.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d =>
                {
                    var span = d.Location.GetLineSpan();
                    return new
                    {
                        line = span.StartLinePosition.Line + 1,
                        column = span.StartLinePosition.Character + 1,
                        message = d.GetMessage()
                    };
                }).ToArray();

            return Json(new
            {
                output = "",
                passed = false,
                message = "Compilation errors occurred",
                errors,
                runtimeError = ""
            });
        }

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());

        string output = "";
        string runtimeError = "";

        try
        {
            var main = assembly.EntryPoint;
            if (main.GetParameters().Length == 0)
                main.Invoke(null, null);
            else
                main.Invoke(null, new object[] { Array.Empty<string>() });

            output = outputWriter.ToString()
                .Replace("\r\n", "\n")
                .Trim();
        }
        catch (Exception ex)
        {
            runtimeError = ex.InnerException?.Message ?? ex.Message;
        }

        // validate
        var validator = new ChallengeValidator();
        var result = validator.Validate(challenge, output, tree);

        Badge? earnedBadge = null;
        if (result.Passed) {
            earnedBadge = session.Current?.Badge;
            session.Complete(); 
        }

        return Json(new
        {
            passed = result.Passed,
            message = result.Message,
            output,
            nextChallenge = session.GameEnded
                ? null
                : session.Current.Title + "\n\n" + session.Current.Guide,
            finished = session.GameEnded,
            runtimeError,
            score = session.Score,
            earnedBadge = earnedBadge != null ? new
            {
                badgeId = earnedBadge.badgeId,
                badgeName = earnedBadge.badgeName,
                badgeDescription = earnedBadge.badgeDescription,
            } : null
        });
    }

    [HttpPost("Skip")]
    public IActionResult Skip()
    {
        var session = GameSessionStore.Get();
        session.Skip();

        return Json(new
        {
            nextChallenge = session.Current == null
                ? null
                : session.Current.Title + "\n\n" + session.Current.Guide,

            skips = session.numberOfSkips,

            finished = session.GameEnded || session.Current == null
        });
    }

    [HttpPost("Scores")]
    public async Task<IActionResult> Scores([FromBody] ChallengeResult2 dto)
    {
        int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

        var challengeProgress = await _services.GetDataByIdAsync<ChallengesProgress>(x => x.ProfileId == Id && x.Challenges.Order == dto.Order, y => y.Challenges);

        if (challengeProgress != null)
        {
            int accumulatedPoints = dto.Score;
            challengeProgress.SetAccumulatedPoints(accumulatedPoints);

            if (dto.EarnedBadge != null)
            {
                var badge = await _services.GetDataByIdAsync<AchievementBadge>(x =>
                x.AchievementBadgeId == challengeProgress.CProgressId
                && x.BadgeId == dto.EarnedBadge.badgeId);

                if (badge == null)
                {
                    var achievementBadge = new AchievementBadge();
                    achievementBadge.SetAchievementBadgeId(challengeProgress.CProgressId);
                    achievementBadge.SetBadgeId(dto.EarnedBadge.badgeId);
                    achievementBadge.SetBadgeName(dto.EarnedBadge.badgeName);
                    achievementBadge.SetDescription(dto.EarnedBadge.badgeDescription);
                    await _services.AddDataAsync<AchievementBadge>(achievementBadge);
                }
            }

            var leaderboard = await _services.GetDataByIdAsync<Leaderboard>(x => x.LeaderboardId == challengeProgress.CProgressId && x.ChallengesProgress.Profiles.ProfileId == Id && x.ChallengesProgress.Challenges.Order == dto.Order, y => y.ChallengesProgress, y => y.ChallengesProgress.Challenges);

            if (leaderboard != null)
            {
                leaderboard.SetTotalPoints(accumulatedPoints);

                await _services.UpdateDataAsync<Leaderboard>(leaderboard);
            }

            var history = await _services.GetDataByIdAsync<History>(h => h.HistoryId == challengeProgress.CProgressId && h.ChallengesProgress.ProfileId == Id && h.logDate.Date == DateTime.Now.Date, h => h.ChallengesProgress, h => h.ChallengesProgress.Profiles);

            if (history != null)
            {
                history.SetDailyPoints(accumulatedPoints);
                await _services.UpdateDataAsync<History>(history);
            }

            await _services.UpdateDataAsync<ChallengesProgress>(challengeProgress);
        }

        return Ok(new
        {
            message = "Data saved successfully."
        });
    }

    [HttpPost("Finish")]
    public async Task<IActionResult> Finish([FromBody] FinishChallenge dto)
    {
        int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

        // get challenge progress
        var challengeProgress = await _services.GetDataByIdAsync<ChallengesProgress>(x => x.ProfileId == Id && x.Challenges.Order == dto.Order, y => y.Challenges);

        if (challengeProgress != null)
        {
            int accumulatedPoints = dto.TimeLeft;
            challengeProgress.SetAccumulatedPoints(accumulatedPoints);

            var history = await _services.GetDataByIdAsync<History>(h => h.HistoryId == challengeProgress.CProgressId && h.ChallengesProgress.ProfileId == Id && h.logDate.Date == DateTime.Now.Date, h => h.ChallengesProgress, h => h.ChallengesProgress.Profiles);
            
            if (history != null)
            {
                history.SetDailyPoints(accumulatedPoints);
                await _services.UpdateDataAsync<History>(history);
            }

            var leaderboard = await _services.GetDataByIdAsync<Leaderboard>(x => x.LeaderboardId == challengeProgress.CProgressId && x.ChallengesProgress.Profiles.ProfileId == Id && x.ChallengesProgress.Challenges.Order == dto.Order, y => y.ChallengesProgress, y => y.ChallengesProgress.Challenges);

            if (leaderboard != null)
            {
                leaderboard.SetTotalPoints(accumulatedPoints);
                if (leaderboard.BestTime < dto.TimeLeft)
                    leaderboard.SetBestTime(accumulatedPoints);

                await _services.UpdateDataAsync<Leaderboard>(leaderboard);
            }

            if (challengeProgress.Time < dto.TimeLeft)
                challengeProgress.UpdateTime(accumulatedPoints);

            await _services.UpdateDataAsync<ChallengesProgress>(challengeProgress);
        }

        return Ok(new
        {
            message = "Data saved successfully." 
        });
    }
}
