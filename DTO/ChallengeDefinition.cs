using Microsoft.CodeAnalysis;

namespace CodieGo_Adventure.DTO
{
    public class ChallengeDefinition
    {
        public int Id { get; set; }
        public string Title { get; set; } // this is difficulty now can't change this yet needs route
        public string ExpectedOutput { get; set; }

        // rule
        public Func<SyntaxNode, bool> RequiredLogic { get; set; }

        // step-by-step
        public string Guide { get; set; }

        public HashSet<DayOfWeek> AvailableDays { get; set; }

        public int ScorePerDifficulty { get; set; }

        public Badge? Badge { get; set; }
    }
}
