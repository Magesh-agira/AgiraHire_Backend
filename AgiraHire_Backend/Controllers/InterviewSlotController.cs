using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgiraHire_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterviewSlotController : ControllerBase
    {
        private readonly IInterviewSlotService _slotService;

        public InterviewSlotController(IInterviewSlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet("getInterviewSlots")]
        public IActionResult GetInterviewSlots()
        {
            try
            {
                var result = _slotService.GetInterviewSlots();
                return Ok(new { result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Ok(new { StatusCode = 500, Message = "An error occurred while fetching interview slots." });
            }
        }


        [HttpPost]
        public IActionResult AddInterviewSlot([FromBody] InterviewSlot slot)
        {
            var result = _slotService.AddInterviewSlot(slot);
            if (result.Success)
            {
                return Ok(new { Data = result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            else
            {
                return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
            }
        }
    }
}
