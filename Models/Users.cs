using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace CodieGo_Adventure.Models
{
    public class Users
    {
        // Properties representing user details
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; private set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; private set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }
        [ValidateNever]
        [DataType(DataType.Date)]
        public DateTime DateRegistered { get; private set; }
        public bool IsNewUser { get; private set; }

        public IEnumerable<LoginRecords> LoginRecords { get; set; }

        // Method to set the registration date to current date
        public void SetRegisterationDate() =>
            DateRegistered = DateTime.Now;

        // Method to enter the user's password
        public void SetPassword(string password) =>
            Password = HashUtility.ComputeSha256Hash(password);

        // Method to enter the user's email
        public void SetEmail(string email) => Email = email;

        // Method to enter the user's username
        public void SetUsername(string username) => Username = username;

        // Method to validate email format
        public bool IsEmailValid(string email)
        {
            Email = email;
            return Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Method to validate password strength
        public bool IsPasswordStrong(string password)
        {
            Password = password;
            return Password.Length >= 8 &&
            Password.Any(char.IsUpper) &&
            (Password.Any(char.IsDigit) ||
            Password.Any(c => !char.IsLetterOrDigit(c)));
        }

        // Method to get masked email for privacy
        public string GetMaskedEmail()
        {
            var atIndex = Email.IndexOf('@');
            if (atIndex <= 1) return Email; // Not enough characters to mask
            var maskedPart = new string('*', atIndex - 1);
            return Email[0] + maskedPart + Email.Substring(atIndex);
        }

        public void SetIsNewUser(bool isNewUser) =>
            IsNewUser = isNewUser;
    }
}
