using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            try
            {
                var addedUser = _user.AddUser(user);
                return Ok(new { User = addedUser.Data, StatusCode = addedUser.ErrorCode, Message = addedUser.Message });
            }
            catch (Exception ex)
            {
                // Log the exception 
                return Ok(new { StatusCode = 500, Message = "An error occurred while adding user." });
            }
        }


        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var result = _user.GetUsers();
                return Ok(new { Users = result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred while fetching users: {ex.Message}");
            }
        }



        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var success = _user.DeleteUser(id);
                return Ok(new { StatusCode = success.ErrorCode, Message = success.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return NotFound("User not found");
            }
        }

    }
}
