using CodieGo_Adventure.Validator;
using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class OutputValidator : ILessonValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, writeLine),
                1 => ValidateStage2(output, writeLine),
                2 => ValidateStage3(output, writeLine),
                _ => new ValidationResult { Passed = false, Message = "Lesson Completed" }
            };
        }

        private ValidationResult ValidateStage1(string output, 
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"Hello World
";
            string expected2 = @"Hello World";

            if (!writeLine.Any()) 
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2) 
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(string output,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"I love programming!
";
            string expected2 = @"I love programming!";

            if (!writeLine.Any()) 
                return Fail("You must use Console.Write or Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(string output,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"ACT is the best
IT school
";
            string expected2 = @"ACT is the best
IT school";

            if (!writeLine.Any()) 
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
