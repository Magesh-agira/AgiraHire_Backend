using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgiraHire_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        public UserController(IUserService user)
        {
            _user = user;

        }

        [HttpPost("addUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                // If there are errors in ModelState, return BadRequest with ModelState errors
                return BadRequest(ModelState);
            }

            var addedUser = _user.AddUser(user);

            if (addedUser == null)
            {
                // If AddUser method returned null, return NotFound or appropriate status code
                return NotFound("Failed to add user");
            }

            return Ok(addedUser); // Return added user entity if successful
        }

        [HttpGet("getUser")]
        public List<User> GetUsers() {
           return _user.GetUsers();
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var success = _user.DeleteUser(id);

            if (!success)
            {
                return NotFound("User not found"); // Return message if user not found
            }

            return Content("Deleted successfull"); // Deletion successful
        }
    }
}
