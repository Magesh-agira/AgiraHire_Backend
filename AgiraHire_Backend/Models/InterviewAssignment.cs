using System.ComponentModel.DataAnnotations;

namespace AgiraHire_Backend.Models
{
    public class InterviewAssignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public  int ApplicantId { get; set; }
        public int SlotId { get; set; }
        public  AssignmentStatus Status { get;set; }
    }

    public enum AssignmentStatus
    {
        Scheduled,
        Completed,
        Rescheduled,
        Cancelled
    }
}
