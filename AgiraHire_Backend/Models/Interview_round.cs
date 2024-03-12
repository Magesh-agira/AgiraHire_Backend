using System.ComponentModel.DataAnnotations;

namespace AgiraHire_Backend.Models
{
    public class Interview_round
    {
        [Key]
        public int RoundID { get; set; }
        public string Round_Name { get; set; }
        public string Description  { get; set; }
    }
}
