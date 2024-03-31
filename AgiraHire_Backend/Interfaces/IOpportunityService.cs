using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System.Collections.Generic;

namespace AgiraHire_Backend.Interfaces
{
    public interface IOpportunityService
    {
        OperationResult<List<opportunity>> GetOpportunities();
        OperationResult<opportunity> AddOpportunity(opportunity opportunity);
    }
}
