using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using AgiraHire_Backend.Response;
using System;

namespace AgiraHire_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterviewRoundController : ControllerBase
    {
        private readonly IInterviewRoundService _roundService;

        public InterviewRoundController(IInterviewRoundService roundService)
        {
            _roundService = roundService;
        }

        [HttpGet("getallrounds")]
        public IActionResult GetAllRounds()
        {
            try
            {
                var result = _roundService.GetAllRounds();
                return Ok(new { result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Ok(new { StatusCode = 500, Message = "An error occurred while fetching rounds." });
            }
        }


        [HttpPost]
        public IActionResult CreateRound([FromBody] Interview_round round)
        {
            var result = _roundService.CreateRound(round);
            if (result.Success)
            {
                return Ok(new { result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            else
            {
                return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRound(int id, [FromBody] Interview_round round)
        {
            try
            {
                var result = _roundService.UpdateRound(id, round);
                if (result.Success)
                {
                    return Ok(new { Data = result.Data, StatusCode = result.ErrorCode, Message = result.Message });
                }
                else
                {
                    return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the round: {ex.Message}");
            }
        }
    }
}
