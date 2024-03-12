using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Interfaces
{
    public interface IInterviewslotService
    {
        public List<InterviewSlot> GetInterviewSlots();
        public InterviewSlot AddInterviewSlot(InterviewSlot interviewSlot);
    }
}
