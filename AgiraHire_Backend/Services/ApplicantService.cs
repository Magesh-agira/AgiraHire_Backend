using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly ApplicationDbContext _context;
        public ApplicantService(ApplicationDbContext context) {
            _context = context;
        }
        public Applicant AddApplicant(Applicant applicant)
        {
           
            var existingOpportunity = _context.Opportunities.Find(applicant.OpportunityId);
            if (existingOpportunity == null)
            {
               
                throw new ArgumentException("Invalid OpportunityId provided.");
            }

            var app = _context.Applicants.Add(applicant);
            _context.SaveChanges();
            return app.Entity;
        }

        public List<Applicant> GetApplicants()
        {
            var applicants = _context.Applicants.ToList();
            return applicants;
        }

        public Applicant UpdateApplicant(int applicantId, ApplicantStatus newStatus)
        {
            var applicant = _context.Applicants.Find(applicantId);
            if (applicant != null)
            {
                applicant.Status = newStatus;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Applicant not found.");
            }

            return applicant;
        }
    }
}
