using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis;

namespace CodieGo_Adventure.Interfaces
{
    public interface IAssessmentValidator
    {
        ValidationResult Validate(string output, SyntaxTree tree, int stage);
    }
}
