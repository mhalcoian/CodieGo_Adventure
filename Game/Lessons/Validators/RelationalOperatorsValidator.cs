using CodieGo_Adventure.Validator;
using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class RelationalOperatorsValidator : ILessonValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            // declared variables with type and initializer
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

            var binaryExpressions = root.DescendantNodes()
                .OfType<BinaryExpressionSyntax>()
                .ToList();

            var ifStatements = root.DescendantNodes()
                .OfType<IfStatementSyntax>()
                .ToList();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, declaredVariables, binaryExpressions, writeLine),
                1 => ValidateStage2(output, declaredVariables, ifStatements, binaryExpressions, writeLine),
                2 => ValidateStage3(output, declaredVariables, ifStatements, binaryExpressions, writeLine),
                3 => ValidateStage4(output, declaredVariables, ifStatements, binaryExpressions, writeLine),
                4 => ValidateStage5(output, declaredVariables, ifStatements, binaryExpressions, writeLine),
                _ => new ValidationResult { Passed = false, Message = "Lesson Completed" }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            Dictionary<string, dynamic> declared,
            List<BinaryExpressionSyntax> binaries,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"True
";
            string expected2 = @"True";

            if (!CheckVariable(declared, "a", "int"))
                return Fail("Declare int variable 'a'.");

            if (!CheckVariable(declared, "b", "int"))
                return Fail("Declare int variable 'b'.");

            if (!CheckVariable(declared, "result", "bool"))
                return Fail("Declare bool variable 'result'.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.GreaterThanExpression)))
                return Fail("Use relational operator: a > b.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs,
            List<BinaryExpressionSyntax> binaries,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"5 is equal to 5
";
            string expected2 = @"5 is equal to 5";

            if (!CheckVariable(declared, "a", "int") || !CheckVariable(declared, "b", "int"))
                return Fail("Declare int variables 'a' and 'b'.");

            if (!ifs.Any())
                return Fail("You must use an if statement.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.EqualsExpression)))
                return Fail("Use equality operator ==.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs,
            List<BinaryExpressionSyntax> binaries,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"5 is not equal to 3
";
            string expected2 = @"5 is not equal to 3";

            if (!CheckVariable(declared, "a", "int") || !CheckVariable(declared, "b", "int"))
                return Fail("Declare int variables 'a' and 'b'.");

            if (!ifs.Any())
                return Fail("You must use an if statement.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.NotEqualsExpression)))
                return Fail("Use not-equal operator !=.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult ValidateStage4(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs,
            List<BinaryExpressionSyntax> binaries,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"8 is greater than or equal to 7
";
            string expected2 = @"8 is greater than or equal to 7";

            if (!CheckVariable(declared, "a", "int") || !CheckVariable(declared, "b", "int"))
                return Fail("Declare int variables 'a' and 'b'.");

            if (!ifs.Any())
                return Fail("You must use an if statement.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.GreaterThanOrEqualExpression)))
                return Fail("Use >= operator.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 4 passed");
        }

        private ValidationResult ValidateStage5(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs,
            List<BinaryExpressionSyntax> binaries,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"4 is less than or equal to 6
";
            string expected2 = @"4 is less than or equal to 6";

            if (!CheckVariable(declared, "a", "int") || !CheckVariable(declared, "b", "int"))
                return Fail("Declare int variables 'a' and 'b'.");

            if (!ifs.Any())
                return Fail("You must use an if statement.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.LessThanOrEqualExpression)))
                return Fail("Use <= operator.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 5 passed");
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
