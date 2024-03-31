using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System.Collections.Generic;

namespace AgiraHire_Backend.Interfaces
{
    public interface IInterviewSlotService
    {
        OperationResult<List<InterviewSlot>> GetInterviewSlots();
        OperationResult<InterviewSlot> AddInterviewSlot(InterviewSlot interviewSlot);
    }
}
