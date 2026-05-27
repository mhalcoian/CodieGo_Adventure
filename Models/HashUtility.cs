using System.Security.Cryptography;
using System.Text;

namespace CodieGo_Adventure.Models
{
    public class HashUtility
    {
        // Method to compute sha256 hash of a given plain text
        public static string ComputeSha256Hash(string plainText)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedText = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                StringBuilder SB = new StringBuilder();
                foreach (var Hashed in hashedText) SB.Append(Hashed.ToString("x2"));
                return SB.ToString();
            }
        }
    }
}
