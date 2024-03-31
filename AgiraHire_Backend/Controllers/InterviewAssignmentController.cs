using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AgiraHire_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterviewAssignmentController : ControllerBase
    {
        private readonly IInterviewAssignmentService _interviewAssignmentService;

        public InterviewAssignmentController(IInterviewAssignmentService interviewAssignmentService)
        {
            _interviewAssignmentService = interviewAssignmentService;
        }

        [HttpPost]
        public IActionResult AddInterviewAssignment([FromBody] InterviewAssignment interviewAssignment)
        {
            var result = _interviewAssignmentService.AddInterviewAssignment(interviewAssignment);
            if (result.Success)
            {
                return Ok(new { InterviewAssignment = result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            else
            {
                return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
            }
        }

        [HttpGet("getinterviewassignment")]
        public IActionResult GetInterviewAssignment()
        {
            try
            {
                var result = _interviewAssignmentService.GetInterviewAssignment();
                return Ok(new { InterviewAssignments = result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred while fetching interview assignments: {ex.Message}");
            }
        }

    }
}
