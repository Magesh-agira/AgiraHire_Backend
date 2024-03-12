using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Services
{
    public class InterviewRoundService : IInterviewRoundService
    {
        private readonly ApplicationDbContext _context;

        public InterviewRoundService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Interview_round> GetAllRounds()
        {
            var rounds= _context.Interview_Rounds.ToList();
            return rounds;
        }



        public Interview_round CreateRound(Interview_round round)
        {
            _context.Interview_Rounds.Add(round);
            _context.SaveChanges();
            return round;
        }

        public Interview_round UpdateRound(int roundId, Interview_round round)
        {
            var existingRound = _context.Interview_Rounds.Find(roundId);
            if (existingRound != null)
            {
                existingRound.Round_Name = round.Round_Name;
                existingRound.Description = round.Description;
                _context.SaveChanges();
                return existingRound;
            }
            else
            {
                throw new ArgumentException("Round not found.");
            }
        }

        public void DeleteRound(int roundId)
        {
            var round = _context.Interview_Rounds.Find(roundId);
            if (round != null)
            {
                _context.Interview_Rounds.Remove(round);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Round not found.");
            }
        }
    }

}
