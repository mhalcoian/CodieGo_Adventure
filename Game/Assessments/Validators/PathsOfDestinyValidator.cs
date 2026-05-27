using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Assessments.Validators
{
    public class PathsOfDestinyValidator : IAssessmentValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var switches = root.DescendantNodes()
                .OfType<SwitchStatementSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, switches),
                1 => ValidateStage2(output, switches),
                2 => ValidateStage3(output, switches),
                _ => new ValidationResult { Passed = false, Message = "Lesson completed." }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            List<SwitchStatementSyntax> switches)
        {
            string expected = "Green";

            if (!switches.Any())
                return Fail("You must use a switch statement.");

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            List<SwitchStatementSyntax> switches)
        {
            string expected = "Fair";

            if (!switches.Any())
                return Fail("You must use a switch statement.");

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(string output,
            List<SwitchStatementSyntax> switches)
        {
            string expected = "Workday";

            if (!switches.Any())
                return Fail("You must use a switch statement.");

            if (output != expected)
                return Fail("Output does not match.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
