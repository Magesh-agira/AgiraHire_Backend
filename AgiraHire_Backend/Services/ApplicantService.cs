using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgiraHire_Backend.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly ApplicationDbContext _context;
        public ApplicantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public OperationResult<Applicant> AddApplicant(Applicant applicant)
        {
            try
            {
                // Custom validations for each field
                if (string.IsNullOrWhiteSpace(applicant.Name))
                    return new OperationResult<Applicant>(null, "Name is required.", 400);

                if (string.IsNullOrWhiteSpace(applicant.EmailId))
                    return new OperationResult<Applicant>(null, "Email is required.", 400);

                if (string.IsNullOrWhiteSpace(applicant.PhoneNumber))
                    return new OperationResult<Applicant>(null, "Phone number is required.", 400);

                if (applicant.OpportunityId <= 0)
                    return new OperationResult<Applicant>(null, "Invalid OpportunityId provided.", 400);

                if (applicant.AppliedDate == default(DateTime))
                    return new OperationResult<Applicant>(null, "Applied date is required.", 400);

                // Check if the referenced opportunity exists
                var existingOpportunity = _context.Opportunities.Find(applicant.OpportunityId);
                if (existingOpportunity == null)
                {
                    return new OperationResult<Applicant>(null, "The provided OpportunityId does not exist.", 400);
                }

                // Additional custom validations can be added as needed

                var addedApplicant = _context.Applicants.Add(applicant);
                _context.SaveChanges();
                return new OperationResult<Applicant>(addedApplicant.Entity, "Applicant added successfully.", 200);
            }
            catch (Exception ex)
            {
                return new OperationResult<Applicant>(null, $"An error occurred: {ex.Message}", 500);
            }
        }
    


        public OperationResult<List<Applicant>> GetApplicants()
        {
            try
            {
                var applicants = _context.Applicants.ToList();
                return new OperationResult<List<Applicant>>(applicants, "Applicants retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Applicant>>(null, $"An error occurred: {ex.Message}", 500);
            }
        }

        public OperationResult<Applicant> UpdateApplicant(int applicantId, ApplicantStatus newStatus)
        {
            try
            {
                var applicant = _context.Applicants.Find(applicantId);
                if (applicant != null)
                {
                    applicant.Status = newStatus;
                    _context.SaveChanges();
                    return new OperationResult<Applicant>(applicant, "Applicant updated successfully.");
                }
                else
                {
                    return new OperationResult<Applicant>(null, "Applicant not found.", 404);
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<Applicant>(null, $"An error occurred: {ex.Message}", 500);
            }
        }
    }
}
