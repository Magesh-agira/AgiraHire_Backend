using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AgiraHire_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpPost]
        // [Authorize]
        public IActionResult AddApplicant([FromBody] Applicant app)
        {
            var result = _applicantService.AddApplicant(app);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Ok( result);
            }
        }

        [HttpGet("Applicants")]
        public IActionResult GetApplicants()
        {
            var result = _applicantService.GetApplicants();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(result.ErrorCode, result);
            }
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateApplicantStatus(int id, [FromBody] ApplicantStatus newStatus)
        {
            var result = _applicantService.UpdateApplicant(id, newStatus);
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
