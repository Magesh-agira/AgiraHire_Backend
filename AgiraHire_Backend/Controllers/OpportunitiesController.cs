using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgiraHire_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class OpportunitiesController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;
        public OpportunitiesController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }
        

        // GET api/<OpportunitiesController>/5
        [HttpGet]
        [Authorize]
        public List<opportunity> GetOpportunities()
        {
            return _opportunityService.GetOpportunities();

        }


        // POST api/<OpportunitiesController>
        [HttpPost]
        [Authorize]
        public opportunity Addopportunity([FromBody] opportunity opp) { 
            var opportunity=_opportunityService.Addopportunity(opp);
            return opportunity;
        }
      

        // PUT api/<OpportunitiesController>/5

    }
}
