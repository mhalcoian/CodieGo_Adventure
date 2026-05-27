using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Interfaces;
using CodieGo_Adventure.Validator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Assessments.Validators
{
    public class MarchOfStepsValidator : IAssessmentValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var forLoops = root.DescendantNodes()
                .OfType<ForStatementSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, forLoops),
                1 => ValidateStage2(output, forLoops),
                2 => ValidateStage3(output, forLoops),
                _ => new ValidationResult { Passed = false, Message = "Lesson completed." }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            List<ForStatementSyntax> loops)
        {
            if (!loops.Any())
                return Fail("You must use a for loop.");

            string expected =
                "5 10 15 20 25";

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            List<ForStatementSyntax> loops)
        {
            if (!loops.Any())
                return Fail("You must use a for loop.");

            string expected =
                "*****";

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(
            string output, 
            List<ForStatementSyntax> loops)
        {
            if (!loops.Any())
                return Fail("You must use a for loop.");

            string expected =
                "\n2\n\n4\n\n6\n\n8\n\n10";

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
