using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Interfaces
{
    public interface IOpportunityService
    {
        public List<opportunity> GetOpportunities();
        public opportunity Addopportunity(opportunity opportunity);
    }
}
