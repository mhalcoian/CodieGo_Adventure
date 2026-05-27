using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Validator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class ArithmeticValidator : ILessonValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

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

            return stage switch
            {
                0 => ValidateStage1(output, writeLine, declaredVariables),
                1 => ValidateStage2(output, writeLine, declaredVariables),
                2 => ValidateStage3(output, writeLine, declaredVariables),
                3 => ValidateStage4(output, writeLine, declaredVariables),
                4 => ValidateStage5(output, writeLine, declaredVariables),
                _ => new ValidationResult { Passed = false, Message = "Lesson Completed" }
            };
        }

        private ValidationResult ValidateStage1(string output, List<InvocationExpressionSyntax> writeLine, Dictionary<string, dynamic> declared)
        {
            string expected1 = @"Sum: 13
";
            string expected2 = @"Sum: 13";

            if (!CheckVariable(declared, "a", "int")) return Fail("Variable 'a' must be int and assigned a value.");
            if (!CheckVariable(declared, "b", "int")) return Fail("Variable 'b' must be int and assigned a value.");
            if (!CheckVariable(declared, "sum", "int")) return Fail("Variable 'sum' must be int and assigned a value.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(string output, List<InvocationExpressionSyntax> writeLine, Dictionary<string, dynamic> declared)
        {
            string expected1 = @"Difference: 6
";
            string expected2 = @"Difference: 6";

            if (!CheckVariable(declared, "a", "int")) return Fail("Variable 'a' must be int and assigned a value.");
            if (!CheckVariable(declared, "b", "int")) return Fail("Variable 'b' must be int and assigned a value.");
            if (!CheckVariable(declared, "diff", "int")) return Fail("Variable 'diff' must be int and assigned a value.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(string output, List<InvocationExpressionSyntax> writeLine, Dictionary<string, dynamic> declared)
        {
            string expected1 = @"Product: 6
";
            string expected2 = @"Product: 6";

            if (!CheckVariable(declared, "a", "int")) return Fail("Variable 'a' must be int and assigned a value.");
            if (!CheckVariable(declared, "b", "int")) return Fail("Variable 'b' must be int and assigned a value.");
            if (!CheckVariable(declared, "prod", "int")) return Fail("Variable 'prod' must be int and assigned a value.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult ValidateStage4(string output, List<InvocationExpressionSyntax> writeLine, Dictionary<string, dynamic> declared)
        {
            string expected1 = @"Quotient: 2.25
";
            string expected2 = @"Quotient: 2.25";

            if (!CheckVariable(declared, "a", "double")) return Fail("Variable 'a' must be double and assigned a value.");
            if (!CheckVariable(declared, "b", "double")) return Fail("Variable 'b' must be double and assigned a value.");
            if (!CheckVariable(declared, "quot", "double")) return Fail("Variable 'quot' must be double and assigned a value.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 4 passed");
        }

        private ValidationResult ValidateStage5(string output, List<InvocationExpressionSyntax> writeLine, Dictionary<string, dynamic> declared)
        {
            string expected1 = @"Remainder: 0
";
            string expected2 = @"Remainder: 0";

            if (!CheckVariable(declared, "a", "int")) return Fail("Variable 'a' must be int and assigned a value.");
            if (!CheckVariable(declared, "b", "int")) return Fail("Variable 'b' must be int and assigned a value.");
            if (!CheckVariable(declared, "mod", "int")) return Fail("Variable 'mod' must be int and assigned a value.");

            if (!writeLine.Any()) return Fail("You must use Console.WriteLine to display output.");

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
