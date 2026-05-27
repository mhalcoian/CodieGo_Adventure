using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodieGo_Adventure.Models
{
    public class ModulesProgress
    {
        [Key]
        public int MProgressId { get; set; }
        public int ProfileId { get; set; }
        [ForeignKey(nameof(ProfileId))]
        public Profiles Profiles { get; set; }
        public int ModuleId { get; set; }
        [ForeignKey(nameof(ModuleId))]
        public Modules Modules { get; set; }
        public bool IsLocked { get; private set; }
        public int CompletedLessons { get; private set; }
        public string Status { get; private set; }

        // Method to update lock status of modules
        public void UpdateLockStatus(bool isLocked) => 
            IsLocked = isLocked;

        public void SetCompletedLessons(int completedLessons) =>
            CompletedLessons += completedLessons;

        [NotMapped]
        public int TrackProgressBar { 
            get {
                if (Modules == null || Modules.TotalLessons == 0)
                    return 0;

                return (CompletedLessons * 100) / Modules.TotalLessons; 
            } 
        }

        public void SetStatus(string status) =>
            Status = status;
    }
}
