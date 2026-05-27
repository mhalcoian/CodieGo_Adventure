using System.ComponentModel.DataAnnotations;

namespace CodieGo_Adventure.Models
{
    public class Challenges
    {
        // Properties representing challenge data
        [Key]
        public int ChallengeId { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public float TimeLimit { get; private set; }
        public int TotalChallenge { get; private set; }
        public int Order { get; private set; }
    }
}
