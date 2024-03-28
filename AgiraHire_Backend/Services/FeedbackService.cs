using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgiraHire_Backend.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public OperationResult<Feedback> AddFeedback(Feedback feedback)
        {
            try
            {
                // Check if the feedback object is null
                if (feedback == null)
                {
                    return new OperationResult<Feedback>(null, "Feedback object cannot be null", 400);
                }

                // Check if the ApplicantId is valid
                var applicant = _context.Applicants.FirstOrDefault(a => a.ApplicantId == feedback.ApplicantId);
                if (applicant == null)
                {
                    return new OperationResult<Feedback>(null, "Invalid Applicant ID", 400);
                }

                // Check if the InterviewerId is valid
                var interviewer = _context.Users.FirstOrDefault(i => i.UserId == feedback.InterviewerId);
                if (interviewer == null)
                {
                    return new OperationResult<Feedback>(null, "Invalid Interviewer ID", 400);
                }

                // Check if the comments are provided
                if (string.IsNullOrWhiteSpace(feedback.Comments))
                {
                    return new OperationResult<Feedback>(null, "Comments are required", 400);
                }

                // Check if the ratings are valid
                if (feedback.Ratings < 0 || feedback.Ratings > 5)
                {
                    return new OperationResult<Feedback>(null, "Ratings must be between 0 and 5", 400);
                }

                // Check if the overall comments are provided
                if (string.IsNullOrWhiteSpace(feedback.Overall_Comments))
                {
                    return new OperationResult<Feedback>(null, "Overall comments are required", 400);
                }

                var addedFeedback = _context.Feedbacks.Add(feedback);
                _context.SaveChanges();
                return new OperationResult<Feedback>(addedFeedback.Entity, "Feedback added successfully", 200);
            }
            catch (Exception ex)
            {
                return new OperationResult<Feedback>(null, $"Failed to add feedback: {ex.Message}", 500);
            }
        }

        public OperationResult<List<Feedback>> GetFeedback()
        {
            var feedbacks = _context.Feedbacks.ToList();

            if (feedbacks != null && feedbacks.Any())
            {
                return new OperationResult<List<Feedback>>(feedbacks, "Feedbacks retrieved successfully", 200);
            }
            else
            {
                return new OperationResult<List<Feedback>>(null, "No feedbacks found", 404);
            }
        }


    }
}
