using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodieGo_Adventure.Models
{
    public class Leaderboard
    {
        [Key]
        public int LeaderboardId { get; private set; }
        [ForeignKey(nameof(LeaderboardId))]
        public ChallengesProgress ChallengesProgress { get; set; }
        public int TotalPoints { get; private set; }
        public float BestTime { get; private set; }

        public void SetLeaderboardId(int leaderboardId) =>
            LeaderboardId = leaderboardId;

        public void SetTotalPoints(int totalPoints) =>
            TotalPoints += totalPoints;

        public void SetBestTime(float bestTime) =>
            BestTime = bestTime;
    }
}
