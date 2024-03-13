using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Interfaces
{
    public interface IInterviewAssignmentService
    {
        public List<InterviewAssignment> GetInterviewAssignment();
        public InterviewAssignment AddInterviewAssignment(InterviewAssignment interviewAssignment);
    }
}
