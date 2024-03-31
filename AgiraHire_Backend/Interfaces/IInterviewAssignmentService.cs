using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System.Collections.Generic;

namespace AgiraHire_Backend.Interfaces
{
    public interface IInterviewAssignmentService
    {
        OperationResult<List<InterviewAssignment>> GetInterviewAssignment();
        OperationResult<InterviewAssignment> AddInterviewAssignment(InterviewAssignment interviewAssignment);
    }
}
