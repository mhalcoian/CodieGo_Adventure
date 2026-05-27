using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;

namespace CodieGo_Adventure.Validator
{
    public interface ILessonValidator
    {
        ValidationResult Validate(string output, SyntaxTree tree, int stage);
    }
}
