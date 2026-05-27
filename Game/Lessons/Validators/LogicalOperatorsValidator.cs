using CodieGo_Adventure.Validator;
using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class LogicalOperatorsValidator : ILessonValidator
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

            var prefixUnary = root.DescendantNodes()
                .OfType<PrefixUnaryExpressionSyntax>()
                .ToList();

            var ifStatements = root.DescendantNodes()
                .OfType<IfStatementSyntax>()
                .ToList();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, declaredVariables, binaryExpressions, ifStatements, writeLine),
                1 => ValidateStage2(output, declaredVariables, binaryExpressions, ifStatements, writeLine),
                2 => ValidateStage3(output, declaredVariables, prefixUnary, ifStatements, writeLine),
                3 => ValidateStage4(output, declaredVariables, binaryExpressions, ifStatements, writeLine),
                _ => new ValidationResult { Passed = false, Message = "Lesson Completed" }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            Dictionary<string, dynamic> declared,
            List<BinaryExpressionSyntax> binaries,
            List<IfStatementSyntax> ifStatements,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"True AND True is true
";
            string expected2 = @"True AND True is true";

            if (!CheckVariable(declared, "a", "bool") || !CheckVariable(declared, "b", "bool"))
                return Fail("Declare bool variables 'a' and 'b'.");

            if (!ifStatements.Any())
                return Fail("You must use an if statement.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.LogicalAndExpression)))
                return Fail("You must use AND operator (&&).");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            Dictionary<string, dynamic> declared,
            List<BinaryExpressionSyntax> binaries,
            List<IfStatementSyntax> ifStatements,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"False OR True is true
";
            string expected2 = @"False OR True is true";

            if (!CheckVariable(declared, "a", "bool") || !CheckVariable(declared, "b", "bool"))
                return Fail("Declare bool variables 'a' and 'b'.");

            if (!ifStatements.Any())
                return Fail("You must use an if statement.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.LogicalOrExpression)))
                return Fail("You must use OR operator (||).");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(
            string output,
            Dictionary<string, dynamic> declared,
            List<PrefixUnaryExpressionSyntax> prefix,
            List<IfStatementSyntax> ifStatements,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"NOT True is false
";
            string expected2 = @"NOT True is false";

            if (!CheckVariable(declared, "a", "bool"))
                return Fail("Declare bool variable 'a'.");
            if (!CheckVariable(declared, "result", "bool"))
                return Fail("Declare bool variable 'result'.");

            if (!ifStatements.Any())
                return Fail("You must use an if statement.");

            if (!prefix.Any(p => p.IsKind(SyntaxKind.LogicalNotExpression)))
                return Fail("You must use NOT operator (!).");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult ValidateStage4(
            string output,
            Dictionary<string, dynamic> declared,
            List<BinaryExpressionSyntax> binaries,
            List<IfStatementSyntax> ifStatements,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected1 = @"(True AND True) OR False is true
";
            string expected2 = @"(True AND True) OR False is true";

            if (!CheckVariable(declared, "a", "bool") ||
                !CheckVariable(declared, "b", "bool") ||
                !CheckVariable(declared, "c", "bool") ||
                !CheckVariable(declared, "result", "bool"))
                return Fail("Declare all required bool variables.");

            if (!ifStatements.Any())
                return Fail("You must use an if statement.");

            bool hasAnd = binaries.Any(b => b.IsKind(SyntaxKind.LogicalAndExpression));
            bool hasOr = binaries.Any(b => b.IsKind(SyntaxKind.LogicalOrExpression));

            if (!hasAnd || !hasOr)
                return Fail("You must combine AND (&&) and OR (||).");

            if (output != expected1 && output != expected2)
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
