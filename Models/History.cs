using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodieGo_Adventure.Models
{
    public class History
    {
        [Key]
        public int HistoryCompositeId { get; set; }
        public int HistoryId { get; private set; }
        [ForeignKey(nameof(HistoryId))]
        public ChallengesProgress ChallengesProgress { get; set; }
        public int DailyPoints { get; private set; }
        public DateTime logDate { get; private set; }

        public void SetHistoryId(int historyId) =>
            HistoryId = historyId;

        public void SetDailyPoints(int dailyPoints) =>
            DailyPoints += dailyPoints;

        public void SetLogDate() =>
            logDate = DateTime.Now;
    }
}
