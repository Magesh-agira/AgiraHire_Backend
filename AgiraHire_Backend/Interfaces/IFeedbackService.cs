using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System.Collections.Generic;

namespace AgiraHire_Backend.Interfaces
{
    public interface IFeedbackService
    {
        OperationResult<List<Feedback>> GetFeedback();
        OperationResult<Feedback> AddFeedback(Feedback feedback);
    }
}
