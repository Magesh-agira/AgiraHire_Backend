using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgiraHire_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterviewSlotController:ControllerBase
    {
        private readonly IInterviewslotService _slotService;

        public InterviewSlotController(IInterviewslotService slotService)
        {
            _slotService = slotService;
        }
        [HttpGet]
        public List<InterviewSlot> GetInterviewSlots()
        {
            return _slotService.GetInterviewSlots();

        }

        [HttpPost]

        public InterviewSlot AddInterviewSlot([FromBody] InterviewSlot slot)
        {
            var interviewslot = _slotService.AddInterviewSlot(slot);
            return interviewslot;
        }

    }
}
