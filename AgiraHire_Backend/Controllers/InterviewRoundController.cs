using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public List<Interview_round> GetAllRounds()
        {
            return _roundService.GetAllRounds();
            
        }

/*        [HttpGet("{id}")]
        public IActionResult GetRoundById(int id)
        {
            var round = _roundService.GetRoundById(id);
            if (round != null)
            {
                return Ok(round);
            }
            else
            {
                return NotFound();
            }
        }*/

        [HttpPost]
        public IActionResult CreateRound(Interview_round round)
        {
            var createdRound = _roundService.CreateRound(round);
            return Ok(createdRound);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRound(int id, Interview_round round)
        {
            try
            {
                var updatedRound = _roundService.UpdateRound(id, round);
                return Ok(updatedRound);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the round.");
            }
        }

/*        [HttpDelete("{id}")]
        public IActionResult DeleteRound(int id)
        {
            try
            {
                _roundService.DeleteRound(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the round.");
            }
        }*/
    }

}
