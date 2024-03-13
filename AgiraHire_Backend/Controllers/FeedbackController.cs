using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgiraHire_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController:ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]

        public Feedback AddFeedback([FromBody] Feedback feedback)
        {
            var interview_feedback = _feedbackService.AddFeedback(feedback);
            return interview_feedback;
        }
    }
}
