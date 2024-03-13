using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Migrations;
using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;
        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Feedback AddFeedback(Feedback feedback)
        {
            var feed = _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return feed.Entity;

        }
    }
}
