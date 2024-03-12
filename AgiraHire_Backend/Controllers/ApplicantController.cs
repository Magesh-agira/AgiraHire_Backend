using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgiraHire_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ApplicantController:ControllerBase
    {

        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpPost]
       // [Authorize]
        public Applicant AddApplicant([FromBody] Applicant app)
        {
            var applicant = _applicantService.AddApplicant(app);
            return applicant;
        }

        [HttpGet]
        public List<Applicant> GetApplicants()
        {
            return _applicantService.GetApplicants();

        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateApplicantStatus(int id, [FromBody] ApplicantStatus newStatus)
        {
            try
            {
                var updatedApplicant = _applicantService.UpdateApplicant(id, newStatus);
                if (updatedApplicant != null)
                {
                    return Ok(updatedApplicant);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the applicant status.");
            }
        }


    }
}
