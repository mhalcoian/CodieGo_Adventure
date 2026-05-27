using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodieGo_Adventure.Models
{
    public class Puzzles
    {
        [Key]
        public int PuzzleId { get; private set; }
        [ForeignKey(nameof(PuzzleId))]
        public ModulesProgress ModulesProgress { get; set; }
        public string PuzzleName { get; private set; }
        public string Description { get; private set; }

        public void SetPuzzleId(int puzzleId) =>
            PuzzleId = puzzleId;

        public void SetPuzzleName(string puzzleName) =>
            PuzzleName = puzzleName;

        public void SetDescription(string desc) =>
            Description = desc;
    }
}
