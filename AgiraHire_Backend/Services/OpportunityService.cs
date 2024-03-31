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
                return new OperationResult<List<opportunity>>(opportunities, "Opportunities retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new OperationResult<List<opportunity>>(null, $"An error occurred: {ex.Message}", 500);
            }
        }

        public OperationResult<opportunity> AddOpportunity(opportunity opportunity)
        {
            try
            {
                var addedOpportunity = _context.Opportunities.Add(opportunity);
                _context.SaveChanges();
                return new OperationResult<opportunity>(addedOpportunity.Entity, "Opportunity added successfully.");
            }
            catch (Exception ex)
            {
                return new OperationResult<opportunity>(null, $"An error occurred: {ex.Message}", 500);
            }
        }
    }
}
