using CodieGo_Adventure.Validator;
using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Lessons.Validators
{
    public class WhileLoopValidator : ILessonValidator
    {
        public ValidationResult Validate(string output, SyntaxTree tree, int stage)
        {
            var root = tree.GetRoot();

            var whiles = root.DescendantNodes()
                .OfType<WhileStatementSyntax>()
                .ToList();

            var binaries = root.DescendantNodes()
                .OfType<BinaryExpressionSyntax>()
                .ToList();

            var postfix = root.DescendantNodes()
                .OfType<PostfixUnaryExpressionSyntax>()
                .ToList();

            var prefix = root.DescendantNodes()
                .OfType<PrefixUnaryExpressionSyntax>()
                .ToList();

            var assignments = root.DescendantNodes()
                .OfType<AssignmentExpressionSyntax>()
                .ToList();

            var writeLine = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .ToList();

            return stage switch
            {
                0 => ValidateStage1(output, whiles, binaries, postfix, prefix, writeLine),
                1 => ValidateStage2(output, whiles, binaries, postfix, prefix, writeLine),
                2 => ValidateStage3(output, whiles, binaries, assignments, writeLine),
                3 => ValidateStage4(output, whiles, binaries, assignments, writeLine),
                4 => ValidateStage5(output, whiles, binaries, assignments, writeLine),
                _ => new ValidationResult { Passed = false, Message = "Lesson completed." }
            };
        }

        private ValidationResult ValidateStage1(
            string output,
            List<WhileStatementSyntax> whiles,
            List<BinaryExpressionSyntax> binaries,
            List<PostfixUnaryExpressionSyntax> post,
            List<PrefixUnaryExpressionSyntax> pre,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected = @"Loop1
Loop2
Loop3
";

            if (!whiles.Any())
                return Fail("You must use a while loop.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.LessThanOrEqualExpression)))
                return Fail("While condition must use <=.");

            if (!post.Any(p => p.IsKind(SyntaxKind.PostIncrementExpression)) &&
                !pre.Any(p => p.IsKind(SyntaxKind.PreIncrementExpression)))
                return Fail("You must increment the variable using ++.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 1 passed");
        }

        private ValidationResult ValidateStage2(
            string output,
            List<WhileStatementSyntax> whiles,
            List<BinaryExpressionSyntax> binaries,
            List<PostfixUnaryExpressionSyntax> post,
            List<PrefixUnaryExpressionSyntax> pre,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected = @"5
4
3
2
1
";

            if (!whiles.Any())
                return Fail("You must use a while loop.");

            if (!binaries.Any(b => b.IsKind(SyntaxKind.GreaterThanOrEqualExpression)))
                return Fail("While condition must use >=.");

            if (!post.Any(p => p.IsKind(SyntaxKind.PostDecrementExpression)) &&
                !pre.Any(p => p.IsKind(SyntaxKind.PreDecrementExpression)))
                return Fail("You must decrement the variable using --.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 2 passed");
        }

        private ValidationResult ValidateStage3(
            string output,
            List<WhileStatementSyntax> whiles,
            List<BinaryExpressionSyntax> binaries,
            List<AssignmentExpressionSyntax> assigns,
            List<InvocationExpressionSyntax> writeLine)
        {
            string expected = @"8
6
4
2
";

            if (!whiles.Any())
                return Fail("You must use a while loop.");

            if (!assigns.Any(a =>
                a.IsKind(SyntaxKind.SubtractAssignmentExpression)))
                return Fail("You must use -= to decrement by step.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 3 passed");
        }

        private ValidationResult ValidateStage4(string output, List<WhileStatementSyntax> whiles, List<BinaryExpressionSyntax> binaries, List<AssignmentExpressionSyntax> assignments, List<InvocationExpressionSyntax> writeLine)
        {
            string expected = @"Post-Test
Post-Test
Post-Test
Post-Test
";

            if (!whiles.Any())
                return Fail("You must use a while loop.");

            if (!binaries.Any(a =>
                a.IsKind(SyntaxKind.PostIncrementExpression)))
                return Fail("You must use ++ to decrement by step.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 4 passed");
        }

        private ValidationResult ValidateStage5(string output, List<WhileStatementSyntax> whiles, List<BinaryExpressionSyntax> binaries, List<AssignmentExpressionSyntax> assignments, List<InvocationExpressionSyntax> writeLine)
        {
            string expected = @"5
4
3
2
";

            if (!whiles.Any())
                return Fail("You must use a while loop.");

            if (!binaries.Any(a =>
                a.IsKind(SyntaxKind.PostDecrementExpression)))
                return Fail("You must use ++ to decrement by step.");

            if (!writeLine.Any())
                return Fail("You must use Console.WriteLine to display output.");

            if (output != expected)
                return Fail("Output does not match. Check the format and unnecessary spacing.");

            return Pass("Stage 5 passed");
        }

        private ValidationResult Fail(string msg) => new ValidationResult { Passed = false, Message = msg };
        private ValidationResult Pass(string msg) => new ValidationResult { Passed = true, Message = msg };
    }
}
