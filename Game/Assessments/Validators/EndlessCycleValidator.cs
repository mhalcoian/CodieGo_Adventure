using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Assessments.Validators
{
    public class EndlessCycleValidator : IAssessmentValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var whiles = root.DescendantNodes()
                .OfType<WhileStatementSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, whiles),
                1 => ValidateStage2(output, whiles),
                2 => ValidateStage3(output, whiles),
                _ => new ValidationResult { Passed = false, Message = "Lesson completed." }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            List<WhileStatementSyntax> whiles)
        {
            if (!whiles.Any())
                return Fail("You must use a while loop.");

            string expected =
                "10\n8\n6\n4\n2\n0";

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            List<WhileStatementSyntax> whiles)
        {
            if (!whiles.Any())
                return Fail("You must use a while loop.");

            string expected =
                "3\n9\n27";

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(
            string output,
            List<WhileStatementSyntax> whiles)
        {
            if (!whiles.Any())
                return Fail("You must use a while loop.");

            string expected =
                "Looping\nLooping\nLooping\nLooping\nLooping\nTotal Loops: 5";

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
