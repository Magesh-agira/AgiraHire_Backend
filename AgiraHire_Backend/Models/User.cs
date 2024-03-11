using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AgiraHire_Backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Employee_Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsDeleted { get; set; }=false;

        public void SetPassword(string password)
        {
            // Generate salt and hash password
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            // Verify password using hashed password
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}
