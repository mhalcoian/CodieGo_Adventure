using CodieGo_Adventure.Controllers;
using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Game.Lessons;
using CodieGo_Adventure.Game.Lessons.Introductions;
using CodieGo_Adventure.Game.Lessons.Validators;
using CodieGo_Adventure.Interfaces;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using CodieGo_Adventure.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;
using System.Reflection;

[ApiController]
[Route("[controller]")]
[ServiceFilter(typeof(RequireSessionFilter))]
public class ModuleController : BaseController
{
    public ModuleController(IGenericServices services) : base(services) { }

    [HttpPost("Compile")]
    public async Task<IActionResult> Compile([FromBody] string code, [FromQuery] string lessonType, [FromQuery] int stage, [FromQuery] int order)
    {
        var outputWriter = new StringWriter();
        //var inputReader = new StringReader(code);
        Console.SetOut(outputWriter);
        //Console.SetIn(inputReader);

        //string tempDir = Path.Combine(Path.GetTempPath(), "CodieGoRunner", Guid.NewGuid().ToString());
        //Directory.CreateDirectory(tempDir);

        //string sourceFile = Path.Combine(tempDir, "Program.cs");
        //string projectFile = Path.Combine(tempDir, "UserProgram.csproj");

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

            output = outputWriter.ToString();
        }
        catch (Exception ex)
        {
            runtimeError = ex.InnerException?.Message ?? ex.Message;
        }

        //// coding method to kill the infinite loop;
        //// execution
        //try
        //{
        //    // Write user code
        //    await System.IO.File.WriteAllTextAsync(sourceFile, code);

        //    // Create minimal project file
        //    await System.IO.File.WriteAllTextAsync(projectFile, @"
        //<Project Sdk=""Microsoft.NET.Sdk"">
        //<PropertyGroup>
        //<OutputType>Exe</OutputType>
        //<TargetFramework>net8.0</TargetFramework>
        //<ImplicitUsings>enable</ImplicitUsings>
        //</PropertyGroup>
        //</Project>");

        //    var process = new Process();

        //    process.StartInfo.FileName = "dotnet";
        //    process.StartInfo.Arguments = $"run --project \"{projectFile}\"";
        //    process.StartInfo.RedirectStandardOutput = true;
        //    process.StartInfo.RedirectStandardInput = true;
        //    process.StartInfo.RedirectStandardError = true;
        //    process.StartInfo.UseShellExecute = false;
        //    process.StartInfo.CreateNoWindow = true;

        //    process.Start();

        //    var outputTask = process.StandardOutput.ReadToEndAsync();
        //    var errorTask = process.StandardError.ReadToEndAsync();

        //    bool finished = process.WaitForExit(30000);
        //    if (!finished)
        //    {
        //        process.Kill(true);
        //        runtimeError = "=== Execution Halted === Possible infinite loop detected.";
        //    }
        //    else
        //    {
        //        output = await outputTask;
        //        runtimeError = await errorTask;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    runtimeError = ex.Message;
        //}
        //finally
        //{
        //    try
        //    {
        //        if (Directory.Exists(tempDir))
        //            Directory.Delete(tempDir, true);
        //    }
        //    catch { }
        //}

        // Validation rules based on lesson type and stage
        ILessonValidator validator = lessonType.ToLower() switch
        {
            "output-statement" => new OutputValidator(),
            "variables-and-data-types" => new VariablesValidator(),
            "arithmetic-operations" => new ArithmeticValidator(),
            "conditional-statement" => new ConditionalValidator(),
            "switch-statement" => new SwitchStatementValidator(),
            "relational-operation" => new RelationalOperatorsValidator(),
            "logical-operation" => new LogicalOperatorsValidator(),
            "while-loop" => new WhileLoopValidator(),
            "for-loop" => new ForLoopValidator(),
            _ => throw new NotImplementedException("Lesson type not implemented.")
        };

        ValidationResult result = validator.Validate(output, tree, stage);

        if (result.Passed)
        {
            int Id = HttpContext.Session.GetInt32("UserId") ?? 0;

            var moduleProgress = await _services.GetDataByIdAsync<ModulesProgress>(x => x.ProfileId == Id && x.Modules.Order == order, y => y.Modules);

            if (moduleProgress != null && moduleProgress.Status == "IN PROGRESS")
            {
                if (moduleProgress.CompletedLessons < moduleProgress.Modules.TotalLessons)
                    moduleProgress.SetCompletedLessons(1);

                if (moduleProgress.CompletedLessons == moduleProgress.Modules.TotalLessons)
                {
                    var puzzles = await _services.GetDataByIdAsync<Puzzles>(x => x.PuzzleId == moduleProgress.MProgressId);
                    if (puzzles == null)
                    {
                        var puzzle = LessonPuzzles.GetPuzzles(order);
                        var puzzleDesc = LessonPuzzles.GetPuzzlesDesc(order);
                        Puzzles newPuzzle = new Puzzles();
                        newPuzzle.SetPuzzleId(moduleProgress.MProgressId);
                        newPuzzle.SetPuzzleName(puzzle);
                        newPuzzle.SetDescription(puzzleDesc);

                        await _services.AddDataAsync<Puzzles>(newPuzzle);
                    }

                    moduleProgress.SetStatus("COMPLETED");

                    int currentOrder = moduleProgress.Modules.Order;

                    var nextChallenge = (await _services.GetAllDataAsync<Challenges>())
                        .Where(c => c.Order == currentOrder)
                        .OrderBy(c => c.Order)
                        .FirstOrDefault();

                    if (nextChallenge != null)
                    {
                        var nextProgress = await _services.GetDataByIdAsync<ChallengesProgress>(
                            x => x.ProfileId == Id && x.ChallengeId == nextChallenge.ChallengeId
                        );

                        if (nextProgress != null && nextProgress.IsLocked)
                        {
                            nextProgress.UpdateLockStatus(false);
                            await _services.UpdateDataAsync<ChallengesProgress>(nextProgress);
                        }
                    }
                }

                await _services.UpdateDataAsync<ModulesProgress>(moduleProgress);
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
        var guide = LessonGuides.GetGuide(lessonType, stage);
        return Json(new { guide });
    }

    [HttpGet("GetTask")]
    public IActionResult GetTask([FromQuery] string lessonType, [FromQuery] int stage)
    {
        var task = LessonGuides.GetTask(lessonType, stage);
        return Json(new { task });
    }

    [HttpGet("GetNote")]
    public IActionResult GetNote([FromQuery] string lessonType, [FromQuery] int stage)
    {
        var note = LessonGuides.GetNotes(lessonType, stage);
        return Json(new { note });
    }

    [HttpGet("GetIntro")]
    public IActionResult GetIntro(string lessonType, int stage)
    {
        ILessonIntroduction intro = lessonType.ToLower() switch
        {
            "output-statement" => new OutputIntroductions(),
            "variables-and-data-types" => new VariableIntroductions(),
            "arithmetic-operations" => new ArithmeticIntroductions(),
            "conditional-statement" => new ConditionalIntroductions(),
            "switch-statement" => new SwitchStatementIntroductions(),
            "relational-operation" => new RelationalOperatorsIntroductions(),
            "logical-operation" => new LogicalOperatorsIntroductions(),
            "while-loop" => new WhileLoopIntroductions(),
            "for-loop" => new ForLoopIntroductions(),
            _ => throw new NotImplementedException("Lesson type not implemented.")
        };

        var lines = intro?.GetIntroduction(stage) ?? new List<string>();

        return Json(new { lines });
    }
}
