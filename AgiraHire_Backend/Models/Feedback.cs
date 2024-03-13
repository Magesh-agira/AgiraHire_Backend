namespace AgiraHire_Backend.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int ApplicantId { get; set; }
        public int InterviewerId { get; set; }

        public string Comments { get; set; }
        public int Ratings { get; set; }
        public string Overall_Comments { get; set; }

    }
}
