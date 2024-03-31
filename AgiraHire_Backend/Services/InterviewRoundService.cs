using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgiraHire_Backend.Services
{
    public class InterviewRoundService : IInterviewRoundService
    {
        private readonly ApplicationDbContext _context;

        public InterviewRoundService(ApplicationDbContext context)
        {
            _context = context;
        }

        public OperationResult<List<Interview_round>> GetAllRounds()
        {
            try
            {
                var rounds = _context.Interview_Rounds.ToList();

                if (rounds != null && rounds.Any())
                {
                    return new OperationResult<List<Interview_round>>(rounds, "Rounds retrieved successfully", 200);
                }
                else
                {
                    return new OperationResult<List<Interview_round>>(null, "No rounds found", 404);
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Interview_round>>(null, $"Failed to retrieve rounds: {ex.Message}", 500);
            }
        }


        public OperationResult<Interview_round> CreateRound(Interview_round round)
        {
            try
            {
                if (round == null)
                {
                    return new OperationResult<Interview_round>(null, "Round object cannot be null", 400);
                }

                if (string.IsNullOrWhiteSpace(round.Round_Name))
                {
                    return new OperationResult<Interview_round>(null, "Round name is required", 400);
                }

                if (string.IsNullOrWhiteSpace(round.Description))
                {
                    return new OperationResult<Interview_round>(null, "Description is required", 400);
                }


                var existingRound = _context.Interview_Rounds.FirstOrDefault(r => r.Round_Name == round.Round_Name);
                if (existingRound != null)
                {
                    return new OperationResult<Interview_round>(null, "Round with the same name already exists", 400);
                } 
                _context.Interview_Rounds.Add(round);
                _context.SaveChanges();

                return new OperationResult<Interview_round>(round, "Round created successfully", 200);
            }
            catch (Exception ex)
            {
                return new OperationResult<Interview_round>(null, $"Failed to create round: {ex.Message}", 500);
            }
        }

        public OperationResult<Interview_round> UpdateRound(int roundId, Interview_round round)
        {
            try
            {
                var existingRound = _context.Interview_Rounds.Find(roundId);
                if (existingRound == null)
                {
                    return new OperationResult<Interview_round>(null, "Round not found", 404);
                }

                if (round == null)
                {
                    return new OperationResult<Interview_round>(null, "Round object cannot be null", 400);
                }

                if (string.IsNullOrWhiteSpace(round.Round_Name))
                {
                    return new OperationResult<Interview_round>(null, "Round name is required", 400);
                }

                if (string.IsNullOrWhiteSpace(round.Description))
                {
                    return new OperationResult<Interview_round>(null, "Description is required", 400);
                }

                existingRound.Round_Name = round.Round_Name;
                existingRound.Description = round.Description;
                _context.SaveChanges();

                return new OperationResult<Interview_round>(existingRound, "Round updated successfully", 200);
            }
            catch (Exception ex)
            {
                return new OperationResult<Interview_round>(null, $"Failed to update round: {ex.Message}", 500);
            }
        }

        
    }
}
