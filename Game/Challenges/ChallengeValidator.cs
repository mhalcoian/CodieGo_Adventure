using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;

namespace CodieGo_Adventure.Game.Challenges
{
    public class ChallengeValidator
    {
        public ValidationResult Validate(ChallengeDefinition challenge, string output, SyntaxTree tree)
        {
            var root = tree.GetRoot();

            // rule check
            if (!challenge.RequiredLogic(root))
            {
                return new ValidationResult
                {
                    Passed = false,
                    Message = "Required logic not detected."
                };
            }

            var userLines = output
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .ToArray();

            var expectedLines = challenge.ExpectedOutput
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .ToArray();

            if (userLines.Length != expectedLines.Length)
            {
                return new ValidationResult
                {
                    Passed = false,
                    Message = $"Output line count mismatch. Expected {expectedLines.Length}, got {userLines.Length}."
                };
            }

            for (int i = 0; i < expectedLines.Length; i++)
            {
                if (userLines[i] != expectedLines[i])
                {
                    return new ValidationResult
                    {
                        Passed = false,
                        Message = $"Line {i + 1} mismatch. Expected: '{expectedLines[i]}', Got: '{userLines[i]}'."
                    };
                }
            }

            return new ValidationResult
            {
                Passed = true,
                Message = "Challenge completed!"
            };
        }
    }
}
