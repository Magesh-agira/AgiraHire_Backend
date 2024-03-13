using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Interfaces
{
    public interface IFeedbackService
    {
        public List<Feedback> GetFeedback();
        public Feedback AddFeedback(Feedback feedback);
    }
}
