using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgiraHire_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class OpportunitiesController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;

        public OpportunitiesController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        // GET api/<OpportunitiesController>
        [HttpGet]
        public IActionResult GetOpportunities()
        {
            var result = _opportunityService.GetOpportunities();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(result.ErrorCode, result);
            }
        }

        // POST api/<OpportunitiesController>
        [HttpPost]
        public IActionResult AddOpportunity([FromBody] opportunity opp)
        {
            var result = _opportunityService.AddOpportunity(opp);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(result.ErrorCode, result);
            }
        }
    }
}
