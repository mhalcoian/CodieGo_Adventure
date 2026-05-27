using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Validator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class ConditionalValidator : ILessonValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var ifStatements = root.DescendantNodes()
                .OfType<IfStatementSyntax>()
                .ToList();

            var declaredVariables = root.DescendantNodes()
                .OfType<VariableDeclarationSyntax>()
                .ToDictionary(
                    v => v.Variables.First().Identifier.Text,
                    v => (dynamic)new
                    {
                        Type = v.Type.ToString(),
                        HasInitializer = v.Variables.First().Initializer != null
                    }
                );

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, declaredVariables, ifStatements, writeLine),
                1 => ValidateStage2(output, declaredVariables, ifStatements, writeLine),
                2 => ValidateStage3(output, declaredVariables, ifStatements, writeLine),
                3 => ValidateStage4(output, declaredVariables, ifStatements, writeLine),
                _ => new ValidationResult { Passed = false, Message = "Lesson Completed" }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"Greater than 5
";
            string expected2 = @"Greater than 5";

            if (!CheckVariable(declared, "num", "int"))
                return Fail("Declare int variable 'num' with value 10.");

            if (!ifs.Any())
                return Fail("You must use an if statement.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"Failed
";
            string expected2 = @"Failed";

            if (!CheckVariable(declared, "num", "int"))
                return Fail("Declare int variable 'num' with value 3.");

            if (!ifs.Any())
                return Fail("You must use an if statement.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"The number is 2
";
            string expected2 = @"The number is 2";

            if (!CheckVariable(declared, "num", "int"))
                return Fail("Declare int variable 'num'.");

            if (!ifs.Any())
                return Fail("You must use an if statement.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult ValidateStage4(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"The number is less than 10
";
            string expected2 = @"The number is less than 10";

            string expected3 = @"The number is less than 20
";
            string expected4 = @"The number is less than 20";

            string expected5 = @"The number is not less than 10 or 20
";
            string expected6 = @"The number is not less than 10 or 20";

            if (!CheckVariable(declared, "Num", "double"))
                return Fail("Declare double variable named Num.");

            if (!ifs.Any())
                return Fail("You must use an if statement.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2 && output != expected3 && output != expected4 && output != expected5 && output != expected6)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 4 passed");
        }

        // check variable exists, type, declared
        private bool CheckVariable(Dictionary<string, dynamic> declared, string name, string type)
        {
            if (!declared.TryGetValue(name, out var info)) return false;
            if (info.Type != type) return false;
            if (!info.HasInitializer) return false;
            return true;
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
