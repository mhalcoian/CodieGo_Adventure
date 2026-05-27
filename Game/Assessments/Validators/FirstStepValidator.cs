using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Assessments.Validators
{
    public class FirstStepValidator : IAssessmentValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>().Any();

            return stage switch
            {
                0 => ValidateStage1(output, writeLine),
                1 => ValidateStage2(output, writeLine),
                2 => ValidateStage3(output, writeLine),
                _ => new ValidationResult { Passed = false, Message = "Assessment Completed" }
            };
        }

        private ValidationResult ValidateStage1(string output, bool writeLine)
        {
            string expected = "Programming is\nFun";

            if (!writeLine) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected) return Fail("Output does not match.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(string output, bool writeLine)
        {
            string expected = "ACT is the\nBest IT\nSchool in\nCebu";

            if (!writeLine) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected) return Fail("Output does not match.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(string output, bool writeLine)
        {
            string expected = "The Quick\nBrown Fox\nJumps Over\nThe\nLazy Dog";

            if (!writeLine) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected) return Fail("Output does not match.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
