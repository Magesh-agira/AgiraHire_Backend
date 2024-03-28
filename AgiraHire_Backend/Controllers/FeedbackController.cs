using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgiraHire_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public IActionResult AddFeedback([FromBody] Feedback feedback)
        {
            var result = _feedbackService.AddFeedback(feedback);
            if (result.Success)
            {
                return Ok(new { Feedback = result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            else
            {
                return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
            }
        }

        [HttpGet("getfeedback")]
        public IActionResult GetFeedback()
        {
            try
            {
                var result = _feedbackService.GetFeedback();
                return Ok(new { result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Ok(new { StatusCode = 500, Message = "An error occurred while fetching feedbacks." });
            }
        }
    }
}
