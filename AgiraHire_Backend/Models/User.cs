using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgiraHire_Backend.Models
{
    public class User 
    {
        [Key]         //attribute indicates that the UserId property is the primary key for the entity.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // property is an identity column in the database, meaning its value will be automatically generated upon insertion.
        public int UserId { get; set; }

        public string? Employee_Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? IsDeleted { get; set; }=false;

        public void SetPassword(string password)
        {
            // Generate salt and hash password  method hashes the provided password using BCrypt hashing algorithm and sets the Password property with the hashed value.
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            // Verify password using hashed password  method compares the provided password with the hashed password stored in the Password property using BCrypt's Verify method, returning true if they match and false otherwise.
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}
