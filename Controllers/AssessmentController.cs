using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Game.Assessments;
using CodieGo_Adventure.Game.Assessments.Validators;
using CodieGo_Adventure.Game.Lessons;
using CodieGo_Adventure.Game.Lessons.Validators;
using CodieGo_Adventure.Interfaces;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using CodieGo_Adventure.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace CodieGo_Adventure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(RequireSessionFilter))]
    public class AssessmentController : BaseController
    {
        public AssessmentController(IGenericServices services) : base(services) { }

        [HttpPost("Compile")]
        public async Task<IActionResult> Compile([FromBody] string code, [FromQuery] string lessonType, [FromQuery] int stage, [FromQuery] int order, [FromQuery] int score)
        {
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
            var emitResult = compilation.Emit(ms);

            // compilation check
            if (!emitResult.Success)
            {
                var errors = emitResult.Diagnostics
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

            // execution
            try
            {
                var main = assembly.EntryPoint;

                if (main.GetParameters().Length == 0)
                    main.Invoke(null, null);
                else
                    main.Invoke(null, new object[] { Array.Empty<string>() });

                output = outputWriter.ToString()
                    .Replace("\r\n", "\n")
                    .TrimEnd();
            }
            catch (Exception ex)
            {
                runtimeError = ex.InnerException?.Message ?? ex.Message;
            }

            // Validation rules based on lesson type and stage
            IAssessmentValidator validator = lessonType.ToLower() switch
            {
                "first-step" => new FirstStepValidator(),
                "memory-awakening" => new MemoryAwakeningValidator(),
                "engine-calibration" => new EngineCalibrationValidator(),
                "path-of-choice" => new PathOfChoiceValidator(),
                "paths-of-destiny" => new PathsOfDestinyValidator(),
                "judgement-protocol" => new JudgementProtocolValidator(),
                "mind-of-logic" => new MindOfLogicValidator(),
                "endless-cycle" => new EndlessCycleValidator(),
                "march-of-steps" => new MarchOfStepsValidator(),
                _ => throw new NotImplementedException("Lesson type not implemented.")
            };

            ValidationResult result = validator.Validate(output, tree, stage);

            int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var challengeProgress = await _services.GetDataByIdAsync<ChallengesProgress>(x => x.ProfileId == Id && x.Challenges.Order == order, y => y.Challenges);

            if (result.Passed)
            {
                if (challengeProgress != null && challengeProgress.Status != "NOT STARTED")
                {
                    if (challengeProgress.CompletedChallenge < challengeProgress.Challenges.TotalChallenge)
                        challengeProgress.SetCompletedChallenge(1);

                    if (challengeProgress.CompletedChallenge == challengeProgress.Challenges.TotalChallenge && challengeProgress.Status == "IN PROGRESS")
                    {
                        challengeProgress.SetStatus("COMPLETED");
                        challengeProgress.SetAssessmentScores(score);
                        challengeProgress.SetSavedAssessmentScores(10);

                        int currentOrder = challengeProgress.Challenges.Order;

                        var nextModule = (await _services.GetAllDataAsync<Modules>())
                            .Where(m => m.Order > currentOrder)
                            .OrderBy(m => m.Order)
                            .FirstOrDefault();

                        if (nextModule != null)
                        {
                            var nextProgress = await _services.GetDataByIdAsync<ModulesProgress>(
                                x => x.ProfileId == Id && x.ModuleId == nextModule.ModuleId
                            );

                            if (nextProgress != null && nextProgress.IsLocked)
                            {
                                nextProgress.UpdateLockStatus(false);
                                await _services.UpdateDataAsync<ModulesProgress>(nextProgress);
                            }
                        }
                    }
                    else if (challengeProgress.CompletedChallenge == challengeProgress.Challenges.TotalChallenge && challengeProgress.Status != "IN PROGRESS")
                    {
                        challengeProgress.SetAssessmentScores(score);
                        challengeProgress.SetSavedAssessmentScores(10);
                    }

                    await _services.UpdateDataAsync<ChallengesProgress>(challengeProgress);
                }
            }
            else
            {
                if (challengeProgress != null && challengeProgress.Status != "NOT STARTED")
                {
                    if (challengeProgress.SavedAssessmentScores > 0)
                        challengeProgress.SetSavedAssessmentScores(score-2);
                    await _services.UpdateDataAsync<ChallengesProgress>(challengeProgress);
                }
            }

            return Json(new
            {
                output,
                passed = result.Passed,
                message = result.Message,
                errors = Array.Empty<object>(),
                runtimeError
            });
        }

        [HttpGet("GetGuide")]
        public IActionResult GetGuide([FromQuery] string lessonType, [FromQuery] int stage)
        {
            var guide = AssessmentGuides.GetGuide(lessonType, stage);
            return Json(new { guide });
        }

        [HttpGet("GetInstruction")]
        public IActionResult GetInstruction([FromQuery] int order)
        {
            var instruction = AssessmentGuides.GetInstructions(order);
            return Json(new { instruction });
        }

        [HttpGet("SavedScore")]
        public async Task<IActionResult> SavedScore([FromQuery] int order)
        {
            int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var challengeProgress = await _services.GetDataByIdAsync<ChallengesProgress>(
                x => x.ProfileId == Id && x.Challenges.Order == order,
                y => y.Challenges
            );

            var currentScore = challengeProgress?.SavedAssessmentScores ?? 10;

            if (challengeProgress != null && challengeProgress.Status == "COMPLETED" && currentScore <= 0)
            {
                challengeProgress.SetSavedAssessmentScores(10);
                await _services.UpdateDataAsync<ChallengesProgress>(challengeProgress);
            }

            return Json(new { currentScore });
        }
    }
}
