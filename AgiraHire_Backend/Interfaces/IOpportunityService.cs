using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System.Collections.Generic;

namespace AgiraHire_Backend.Interfaces
{
    public interface IOpportunityService
    {
        OperationResult<List<opportunity>> GetOpportunities();
        OperationResult<opportunity> AddOpportunity(opportunity opportunity);

        OperationResult<opportunity> GetOpportunityById(int id);

        OperationResult<opportunity> UpdateOpportunity(int id, opportunity updatedOpportunity);

    }
}
