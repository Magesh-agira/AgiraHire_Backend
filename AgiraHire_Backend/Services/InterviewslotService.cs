using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgiraHire_Backend.Services
{
    public class InterviewSlotService : IInterviewSlotService
    {
        private readonly ApplicationDbContext _context;

        public InterviewSlotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public OperationResult<InterviewSlot> AddInterviewSlot(InterviewSlot interviewSlot)
        {
            try
            {
                // Check if the interviewSlot object is null
                if (interviewSlot == null)
                {
                    return new OperationResult<InterviewSlot>(null, "Interview slot object cannot be null", 400);
                }

                // Check if the start time is in the past
                if (interviewSlot.StartTime <= DateTime.Now)
                {
                    return new OperationResult<InterviewSlot>(null, "Start time must be in the future", 400);
                }

                // Check if the end time is after the start time
                if (interviewSlot.EndTime <= interviewSlot.StartTime)
                {
                    return new OperationResult<InterviewSlot>(null, "End time must be later than start time", 400);
                }

                // Check if the venue is provided
                if (string.IsNullOrWhiteSpace(interviewSlot.Venue))
                {
                    return new OperationResult<InterviewSlot>(null, "Venue is required", 400);
                }

                // Check if the InterviewerId is valid
                if (!_context.Users.Any(u => u.UserId == interviewSlot.InterviewerId))
                {
                    return new OperationResult<InterviewSlot>(null, "Invalid Interviewer ID", 400);
                }

                // Check if the RoundId is valid
                if (!_context.Interview_Rounds.Any(r => r.RoundID == interviewSlot.RoundId))
                {
                    return new OperationResult<InterviewSlot>(null, "Invalid Round ID", 400);
                }

                // Add the interview slot to the database
                var addedSlot = _context.InterviewSlots.Add(interviewSlot);
                _context.SaveChanges();

                // Return success result with added interview slot
                return new OperationResult<InterviewSlot>(addedSlot.Entity, "Interview slot added successfully", 200);
            }
            catch (Exception ex)
            {
                // Return error result if an exception occurs
                return new OperationResult<InterviewSlot>(null, $"Failed to add interview slot: {ex.Message}", 500);
            }
        }
        public OperationResult<List<InterviewSlot>> GetInterviewSlots()
        {
            try
            {
                var interviewSlots = _context.InterviewSlots.ToList();

                if (interviewSlots != null && interviewSlots.Any())
                {
                    return new OperationResult<List<InterviewSlot>>(interviewSlots, "Interview slots retrieved successfully", 200);
                }
                else
                {
                    return new OperationResult<List<InterviewSlot>>(null, "No interview slots found", 404);
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<List<InterviewSlot>>(null, $"Failed to retrieve interview slots: {ex.Message}", 500);
            }
        }

    }
}
