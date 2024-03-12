using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgiraHire_Backend.Models
{
    public class InterviewSlot
    {
        [Key]
        public int SlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Venue { get; set; }

        public int InterviewerId { get; set; } 

        public int RoundId { get; set; } 
    }
}
