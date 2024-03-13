using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgiraHire_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterviewAssignmentController
    {
        private readonly IInterviewAssignmentService _interviewAssignmentService;

        public InterviewAssignmentController(IInterviewAssignmentService interviewAssignmentService)
        {
            _interviewAssignmentService = interviewAssignmentService;
        }

        [HttpPost]

        public InterviewAssignment AddInterviewAssignment([FromBody] InterviewAssignment interviewAssignment)
        {
            var interview_assignment = _interviewAssignmentService.AddInterviewAssignment(interviewAssignment);
            return interview_assignment;
        }

        [HttpGet]
        public List<InterviewAssignment> GetInterviewAssignment()
        {
            return _interviewAssignmentService.GetInterviewAssignment();

        }
    }
}
