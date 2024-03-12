using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Services
{
    public class InterviewslotService:IInterviewslotService
    {
        private readonly ApplicationDbContext _context;
        public InterviewslotService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<InterviewSlot> GetInterviewSlots()
        {
            var interviewslots = _context.InterviewSlots.ToList();
            return interviewslots;
        }

        public InterviewSlot AddInterviewSlot(InterviewSlot interviewslot)
        {
            var slot = _context.InterviewSlots.Add(interviewslot);
            _context.SaveChanges();
            return slot.Entity;
        }
    }
}
