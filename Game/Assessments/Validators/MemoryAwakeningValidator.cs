using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Interfaces;
using CodieGo_Adventure.Validator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Assessments.Validators
{
    public class MemoryAwakeningValidator : IAssessmentValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>().Any();

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
                _ => new ValidationResult { Passed = false, Message = "Assessment Completed" }
            };
        }

        private ValidationResult ValidateStage1(string output, bool writeLine,
            Dictionary<string, dynamic> declared)
        {
            string expected = "Last Name: Delos Reyes\nGrade: 94.5\nAttendance: 20\nEnrolled: True";

            if (!CheckVariable(declared, "string")) return Fail("You must declare a string data type variable and assign it a value.");
            if (!CheckVariable(declared, "double")) return Fail("You must declare a double data type variable and assign it a value.");
            if (!CheckVariable(declared, "int")) return Fail("You must declare a int data type variable and assign it a value.");
            if (!CheckVariable(declared, "bool")) return Fail("You must declare a bool data type variable and assign it a value.");

            if (!writeLine) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected) return Fail("Output does not match");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(string output, bool writeLine,
            Dictionary<string, dynamic> declared)
        {
            string expected = "First Name: Tibursio\nMiddle Initial: B\nLast Name: Binangkal";

            if (!CheckVariable(declared, "string")) return Fail("You must declare a string data type variable and assign it a value.");
            if (!CheckVariable(declared, "char")) return Fail("You must declare a char data type variable and assign it a value.");

            if (!writeLine) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected) return Fail("Output does not match");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(string output, bool writeLine,
            Dictionary<string, dynamic> declared)
        {
            string expected = "Product Name: Coconut Soap\nProduct ID: 676767\nPrice: 59.99";

            if (!CheckVariable(declared, "string")) return Fail("You must declare a string data type variable and assign it a value.");
            if (!CheckVariable(declared, "int")) return Fail("You must declare a int data type variable and assign it a value.");
            if (!CheckVariable(declared, "double")) return Fail("You must declare a double data type variable and assign it a value.");

            if (!writeLine) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected) return Fail("Output does not match");

            return Pass("Stage 3 passed");
        }

        // check variable exists, type, declared
        private bool CheckVariable(Dictionary<string, dynamic> declared, string type)
        {
            return declared.Values.Any(info => info.Type == type && info.HasInitializer);
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
