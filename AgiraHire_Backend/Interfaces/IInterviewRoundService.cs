using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System.Collections.Generic;

namespace AgiraHire_Backend.Interfaces
{
    public interface IInterviewRoundService
    {
        OperationResult<List<Interview_round>> GetAllRounds();

        OperationResult<Interview_round> CreateRound(Interview_round round);

        OperationResult<Interview_round> UpdateRound(int roundId, Interview_round round);
    }
}
