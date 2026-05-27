using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodieGo_Adventure.Models
{
    public class AchievementBadge
    {
        [Key]
        public int AchievementCompositeId { get; set; }
        public int AchievementBadgeId { get; private set; }
        [ForeignKey(nameof(AchievementBadgeId))]
        public ChallengesProgress ChallengesProgress { get; set; }
        public int BadgeId { get; private set; }
        public string BadgeName { get; private set; }
        public string Description { get; private set; }

        public void SetAchievementBadgeId(int achievementBadgeId) =>
            AchievementBadgeId = achievementBadgeId;

        public void SetBadgeId(int badgeId) =>
            BadgeId = badgeId;

        public void SetBadgeName(string badgeName) => 
            BadgeName = badgeName;

        public void SetDescription(string desc) =>
            Description = desc;
    }
}
