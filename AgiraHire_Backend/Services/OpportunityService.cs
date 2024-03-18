using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace AgiraHire_Backend.Services
{
    public class OpportunityService:IOpportunityService
    {
        private readonly ApplicationDbContext _context;
        public OpportunityService( ApplicationDbContext context)
        {
            _context = context;
        }

        public List<opportunity> GetOpportunities()
        {
            var opportunities=_context.Opportunities.ToList();
            
            return opportunities;
           
        }

        public opportunity Addopportunity(opportunity opportunity)
        {
            var opp=_context.Opportunities.Add(opportunity);
            _context.SaveChanges();
            return opp.Entity;
        }
    }
}
