using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetOpportunities()
        {
            try
            {
                var result = _opportunityService.GetOpportunities();
                return Ok(new { Data = result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { StatusCode = 500, Message = $"An error occurred: {ex.Message}" });
            }
        }


        [HttpPost]
        public IActionResult AddOpportunity([FromBody] opportunity opportunity)
        {
            try
            {
                var result = _opportunityService.AddOpportunity(opportunity);
                return Ok(new { Opportunity = result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}
