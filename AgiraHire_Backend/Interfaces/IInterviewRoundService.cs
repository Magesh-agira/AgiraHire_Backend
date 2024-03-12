using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Interfaces
{
    public interface IInterviewRoundService
    {
        public List<Interview_round> GetAllRounds();

        Interview_round CreateRound(Interview_round round);
        Interview_round UpdateRound(int roundId, Interview_round round);

    }

}
