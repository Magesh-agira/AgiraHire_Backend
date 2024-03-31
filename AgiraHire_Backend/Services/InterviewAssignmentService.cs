using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgiraHire_Backend.Services
{
    public class InterviewAssignmentService : IInterviewAssignmentService
    {
        private readonly ApplicationDbContext _context;

        public InterviewAssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public OperationResult<InterviewAssignment> AddInterviewAssignment(InterviewAssignment interviewAssignment)
        {
            try
            {
                // Check if the interviewAssignment object is null
                if (interviewAssignment == null)
                {
                    return new OperationResult<InterviewAssignment>(null, "Interview assignment object cannot be null", 400);
                }

                // Check if the ApplicantId is valid
                var applicant = _context.Applicants.FirstOrDefault(a => a.ApplicantId == interviewAssignment.ApplicantId);
                if (applicant == null)
                {
                    return new OperationResult<InterviewAssignment>(null, "Invalid Applicant ID", 400);
                }

                // Check if the SlotId is valid
                var slot = _context.InterviewSlots.FirstOrDefault(s => s.SlotId == interviewAssignment.SlotId);
                if (slot == null)
                {
                    return new OperationResult<InterviewAssignment>(null, "Invalid Slot ID", 400);
                }

                var addedAssignment = _context.InterviewAssignments.Add(interviewAssignment);
                _context.SaveChanges();
                return new OperationResult<InterviewAssignment>(addedAssignment.Entity, "Interview assignment added successfully", 200);
            }
            catch (Exception ex)
            {
                return new OperationResult<InterviewAssignment>(null, $"Failed to add interview assignment: {ex.Message}", 500);
            }
        }

        public OperationResult<List<InterviewAssignment>> GetInterviewAssignment()
        {
            var assignments = _context.InterviewAssignments.ToList();

            if (assignments != null && assignments.Any())
            {
                return new OperationResult<List<InterviewAssignment>>(assignments, "Interview assignments retrieved successfully", 200);
            }

            return new OperationResult<List<InterviewAssignment>>(null, "No interview assignments found", 404);
        }


    }
}
