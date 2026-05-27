using CodieGo_Adventure.Validator;
using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class VariablesValidator : ILessonValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

            // Check variable declarations
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

        private ValidationResult ValidateStage1(string output, List<InvocationExpressionSyntax> writeLine,
            Dictionary<string, dynamic> declared)
        {
            string expected1 = @"This is a string
";
            string expected2 = @"This is a string";

            if (!CheckVariable(declared, "text", "string")) return Fail("Variable 'text' must be string and assigned a value.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(string output, List<InvocationExpressionSyntax> writeLine,
            Dictionary<string, dynamic> declared)
        {
            string expected1 = @"A
";
            string expected2 = @"A";

            if (!CheckVariable(declared, "grade", "char")) return Fail("Variable 'grade' must be char and assigned a value.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(string output, List<InvocationExpressionSyntax> writeLine,
            Dictionary<string, dynamic> declared)
        {
            string expected1 = @"3
";
            string expected2 = @"3";

            if (!CheckVariable(declared, "num", "int")) return Fail("Variable 'num' must be int and assigned a value.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult ValidateStage4(string output, List<InvocationExpressionSyntax> writeLine,
            Dictionary<string, dynamic> declared)
        {
            string expected1 = @"12.51
";
            string expected2 = @"12.51";

            if (!CheckVariable(declared, "num", "double")) return Fail("Variable 'num' must be double and assigned a value.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected1 && output != expected2)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 4 passed");
        }

        private ValidationResult ValidateStage5(string output, List<InvocationExpressionSyntax> writeLine,
            Dictionary<string, dynamic> declared)
        {
            string expected1 = @"True
";
            string expected2 = @"True";

            if (!CheckVariable(declared, "isBoolean", "bool")) return Fail("Variable 'isBoolean' must be bool and assigned a value.");

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
