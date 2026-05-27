using CodieGo_Adventure.Game.Lessons.Guides;
using CodieGo_Adventure.Game.Lessons.Puzzle;

namespace CodieGo_Adventure.Game.Lessons
{
    public class LessonPuzzles
    {
        public static string GetPuzzles(int order) =>
            PuzzleBadge.Puzzles.GetValueOrDefault(order) ?? "No puzzle found";

        public static string GetPuzzlesDesc(int order) =>
            PuzzleBadgeDescription.PuzzlesDesc.GetValueOrDefault(order) ?? "No puzzle description found";
    }
}
