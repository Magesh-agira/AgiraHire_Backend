using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Services
{
    public class InterviewAssignmentService
    {
        private readonly ApplicationDbContext _context;
        public InterviewAssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public InterviewAssignment AddInterviewAssignment(InterviewAssignment interviewAssignment)
        {
            var assignment = _context.InterviewAssignments.Add(interviewAssignment);
            _context.SaveChanges();
            return assignment.Entity;
        }

        public List<InterviewAssignment> GetInterviewAssignment()
        {
            var interviewAssignments = _context.InterviewAssignments.ToList();
            return interviewAssignments;
        }
    }
}

       
      

       
