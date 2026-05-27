using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodieGo_Adventure.Models
{
    public class LoginRecords
    {
        // Properties representing login record details
        [Key]
        public int RecordId { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Users Users { get; set; }
        [ValidateNever]
        public Guid? SessionId { get; private set; }
        [ValidateNever]
        [DataType(DataType.Date)]
        public DateTime LoginDateTime { get; private set; }
        [ValidateNever]
        public string Status { get; private set; } // online, offline

        // Method to set the login date and time to current date and time
        public void SetLoginDateTime() =>
            LoginDateTime = DateTime.Now;

        // Method to set the status of the login record
        public void SetStatus(string newStatus) =>
            Status = newStatus;

        // Method to set the session ID
        public void SetSessionId(Guid sessionId) =>
            SessionId = sessionId;
    }
}
