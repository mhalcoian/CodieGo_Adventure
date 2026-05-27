using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodieGo_Adventure.Models
{
    public class PasswordResetTokens
    {
        [Key]
        public int PasswordResetTokenId { get; set; }
        public int UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public Users User { get; set; }
        public string Token { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public DateTime? UsedDate { get; private set; }

        public void SetUserId(int userId) =>
            UserId = userId;

        public void SetToken(string token) =>
            Token = token;

        public void SetExperationDate() =>
            ExpirationDate = DateTime.Now.AddHours(1);

        public void SetUsedDate() =>
            UsedDate = DateTime.Now;
    }
}
