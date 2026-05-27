using CodieGo_Adventure.Validator;
using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class ForLoopValidator : ILessonValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var forLoops = root.DescendantNodes()
                .OfType<ForStatementSyntax>()
                .ToList();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, forLoops, writeLine),
                1 => ValidateStage2(output, forLoops, writeLine),
                _ => new ValidationResult { Passed = false, Message = "Lesson completed." }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            List<ForStatementSyntax> loops,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected = @"1
2
3
4
5
";

            if (!loops.Any())
                return Fail("You must use a for loop.");

            var loop = loops.First();

            if (loop.Condition == null)
                return Fail("For loop must have a condition.");

            if (!loop.Incrementors.Any())
                return Fail("For loop must have an increment.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            List<ForStatementSyntax> loops,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected = @"5
4
3
2
1
";

            if (!loops.Any())
                return Fail("You must use a for loop.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
