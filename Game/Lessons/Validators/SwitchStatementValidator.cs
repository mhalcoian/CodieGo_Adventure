using CodieGo_Adventure.Validator;
using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class SwitchStatementValidator : ILessonValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var switches = root.DescendantNodes()
                .OfType<SwitchStatementSyntax>()
                .ToList();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, switches, writeLine),
                1 => ValidateStage2(output, switches, writeLine),
                _ => new ValidationResult { Passed = false, Message = "Lesson completed." }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            List<SwitchStatementSyntax> switches,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"Option 1
";
            string expected2 = @"Option 1";

            if (!switches.Any())
                return Fail("You must use a switch statement.");

            var sw = switches.First();

            if (sw.Expression.ToString() != "choice")
                return Fail("Switch must use variable 'choice'.");

            bool hasCase1 =
                sw.Sections.Any(s =>
                    s.Labels
                     .OfType<CaseSwitchLabelSyntax>()
                     .Any(l => l.Value.ToString() == "1"));

            if (!hasCase1)
                return Fail("Switch must contain case 1.");

            bool hasBreak =
                sw.Sections.Any(s =>
                    s.Statements.Any(st => st is BreakStatementSyntax));

            if (!hasBreak)
                return Fail("Each case must include a break statement.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            List<SwitchStatementSyntax> switches,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"Invalid Day
";
            string expected2 = @"Invalid Day";

            if (!switches.Any())
                return Fail("You must use a switch statement.");

            var sw = switches.First();

            if (sw.Expression.ToString() != "day")
                return Fail("Switch must use variable 'day'.");

            bool hasDefault =
                sw.Sections.Any(s =>
                    s.Labels.OfType<DefaultSwitchLabelSyntax>().Any());

            if (!hasDefault)
                return Fail("Switch must contain a default case.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
