using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodieGo_Adventure.Models
{
    public class ChallengesProgress
    {
        // Properties representing history/progress of challenges for profiles
        [Key]
        public int CProgressId { get; set; }
        public int ProfileId { get; set; }
        [ForeignKey(nameof(ProfileId))]
        public Profiles Profiles { get; set; }
        public int ChallengeId { get; set; }
        [ForeignKey(nameof(ChallengeId))]
        public Challenges Challenges { get; set; }
        public int AccumulatedPoints { get; private set; }
        public bool IsLocked { get; private set; }
        public float Time { get; private set; }
        public bool IsDaily { get; private set; }
        public DateTime? LastDailyReset { get; private set; }
        public int CompletedChallenge { get; private set; }
        public string Status { get; private set; }
        public int AssessmentScores { get; private set; }
        public int SavedAssessmentScores { get; private set; }

        [NotMapped]
        public int TrackProgressBar
        {
            get
            {
                if (Challenges == null || Challenges.TotalChallenge == 0)
                    return 0;

                return (CompletedChallenge * 100) / Challenges.TotalChallenge;
            }
        }

        // Method to set accumulated points each challenges
        public void SetAccumulatedPoints(int accumulatedPoints) => 
            AccumulatedPoints += accumulatedPoints;

        // Method to update lock status of challenges
        public void UpdateLockStatus(bool isLocked) => 
            IsLocked = isLocked;

        // Method to update time for challenges
        public void UpdateTime(float time) => 
            Time = time;

        public void SetIsDaily(bool isDaily) =>
            IsDaily = isDaily;

        public void SetLastDailyReset(DateTime resetTime) =>
            LastDailyReset = resetTime;

        public void SetCompletedChallenge(int completedChallenge) =>
            CompletedChallenge += completedChallenge;

        public void SetStatus(string status) =>
            Status = status;

        public void SetAssessmentScores(int assessmentScores) =>
            AssessmentScores = assessmentScores;

        public void SetSavedAssessmentScores(int savedAssessmentScores) =>
            SavedAssessmentScores = savedAssessmentScores;
    }
}
