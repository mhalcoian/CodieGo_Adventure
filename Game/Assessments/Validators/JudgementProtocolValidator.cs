using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Interfaces;
using CodieGo_Adventure.Validator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Assessments.Validators
{
    public class JudgementProtocolValidator : IAssessmentValidator
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

            var ifStatements = root.DescendantNodes()
                .OfType<IfStatementSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, declaredVariables, ifStatements),
                1 => ValidateStage2(output, declaredVariables, ifStatements),
                2 => ValidateStage3(output, declaredVariables, ifStatements),
                _ => new ValidationResult { Passed = false, Message = "Lesson Completed" }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs)
        {
            string expected = "Minor";

            if (!CheckVariable(declared, "int"))
                return Fail("You must declare a int data type variable and assign it a value.");

            if (!ifs.Any(i => i.Condition
                .DescendantNodesAndSelf()
                .OfType<BinaryExpressionSyntax>()
                .Any()))
                return Fail("You must use an if statement. with a proper use of conditions.");

            //if (!writeLine) return Fail("You must use Console.WriteLine to display output.");

            if (output != expected)
                return Fail("Output does not match");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs)
        {
            string expected = "Low Stock";

            if (!CheckVariable(declared, "int"))
                return Fail("You must declare a int data type variable and assign it a value.");

            if (!ifs.Any(i => i.Condition
                .DescendantNodesAndSelf()
                .OfType<BinaryExpressionSyntax>()
                .Any()))
                return Fail("You must use an if statement. with a proper use of conditions.");

            if (output != expected)
                return Fail("Output does not match");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(
            string output,
            Dictionary<string, dynamic> declared,
            List<IfStatementSyntax> ifs)
        {
            string expected = "Odd Number";

            if (!CheckVariable(declared, "int"))
                return Fail("You must declare a int data type variable and assign it a value.");

            if (!ifs.Any(i => i.Condition
                .DescendantNodesAndSelf()
                .OfType<BinaryExpressionSyntax>()
                .Any()))
                return Fail("You must use an if statement. with a proper use of conditions.");

            if (output != expected)
                return Fail("Output does not match");

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
