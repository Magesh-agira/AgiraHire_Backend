using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgiraHire_Backend.Services
{
    public class OpportunityService : IOpportunityService
    {
        private readonly ApplicationDbContext _context;

        public OpportunityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public OperationResult<List<opportunity>> GetOpportunities()
        {
            try
            {
                var opportunities = _context.Opportunities.ToList();

                if (opportunities != null && opportunities.Any())
                {
                    return new OperationResult<List<opportunity>>(opportunities, "Opportunities retrieved successfully", 200);
                }
                else
                {
                    return new OperationResult<List<opportunity>>(null, "No opportunities found", 404);
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<List<opportunity>>(null, $"Failed to retrieve opportunities: {ex.Message}", 500);
            }
        }


        public OperationResult<opportunity> AddOpportunity(opportunity opportunity)
        {
            try
            {
                // Check if the opportunity object is null
                if (opportunity == null)
                {
                    return new OperationResult<opportunity>(null, "Opportunity object cannot be null", 400);
                }

                // Validate position
                if (string.IsNullOrWhiteSpace(opportunity.Position))
                {
                    return new OperationResult<opportunity>(null, "Position is required", 400);
                }

                // Validate location
                if (string.IsNullOrWhiteSpace(opportunity.Location))
                {
                    return new OperationResult<opportunity>(null, "Location is required", 400);
                }

                // Validate employment type
                if (string.IsNullOrWhiteSpace(opportunity.Employment_Type))
                {
                    return new OperationResult<opportunity>(null, "Employment type is required", 400);
                }

                // Validate qualification
                if (string.IsNullOrWhiteSpace(opportunity.Qualification))
                {
                    return new OperationResult<opportunity>(null, "Qualification is required", 400);
                }

                // Validate salary
                if (string.IsNullOrWhiteSpace(opportunity.Salary))
                {
                    return new OperationResult<opportunity>(null, "Salary is required", 400);
                }

                // Validate date posted
                if (opportunity.Date_Posted == null)
                {
                    return new OperationResult<opportunity>(null, "Date posted is required", 400);
                }

                // Validate number of openings
                if (opportunity.No_Of_Openings <= 0)
                {
                    return new OperationResult<opportunity>(null, "Number of openings must be greater than zero", 400);
                }

                // Validate status (if needed)
                // Add custom validation logic for other fields if necessary

                var addedOpportunity = _context.Opportunities.Add(opportunity);
                _context.SaveChanges();
                return new OperationResult<opportunity>(addedOpportunity.Entity, "Opportunity added successfully", 200);
            }
            catch (Exception ex)
            {
                return new OperationResult<opportunity>(null, $"Failed to add opportunity: {ex.Message}", 500);
            }
        }

    }
}
