using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace CodieGo_Adventure.Models
{
    public class Profiles
    {
        // Properties representing the users profile
        [Key]
        public int ProfileId { get; private set; }
        [ForeignKey(nameof(ProfileId))]
        public Users Users { get; set; }
        public byte[]? Image { get; private set; }
        public string? ImageName { get; private set; }
        public string? ImageExtension { get; private set; }
        public string DisplayName { get; private set; }
        public string? Bio { get; private set; }

        [NotMapped]
        public IEnumerable<ModulesProgress> ModuleProgressCollection { get; set; }
        [NotMapped]
        public IEnumerable<ChallengesProgress> ChallengeProgressCollection { get; set; }
        [NotMapped]
        public IEnumerable<Leaderboard> LeaderboardCollection { get; set; }
        [NotMapped]
        public IEnumerable<Puzzles> PuzzlesCollection { get; set; }
        [NotMapped]
        public IEnumerable<AchievementBadge> AchievementBadgeCollection { get; set; }
        [NotMapped]
        public IEnumerable<History> HistoryCollection { get; set; }

        // Method to set users id
        public void SetProfileId(int profileId) =>
            ProfileId = profileId;

        // Method to accept image by converting into a base64string
        public async void ConvertToBase64String(IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(ms);
                    Image = ms.ToArray();
                    ImageName = Path.GetFileName(ImageFile.FileName);
                    ImageExtension = Path.GetExtension(ImageFile.FileName);
                }
            }
        }

        [NotMapped]
        [ValidateNever]
        public string Base64StringImage
        {
            get => Image != null ? $"data:image/{ImageExtension?.TrimStart('.')};base64,{Convert.ToBase64String(Image)}"
                : "/images/characters/default.jpg";
        }

        // Method to set the user display name
        public void SetDiplayName(string displayName) => 
            DisplayName = displayName;

        // Method to set bio
        public void SetBio(string bio) => Bio = bio;
    }
}
