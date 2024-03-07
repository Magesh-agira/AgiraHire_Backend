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
    }
}
