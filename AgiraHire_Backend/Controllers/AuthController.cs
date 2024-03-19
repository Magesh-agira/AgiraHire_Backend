using Microsoft.AspNetCore.Mvc;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System;
using AgiraHire_Backend.Response.AgiraHire_Backend.Responses;

namespace AgiraHire_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var result = _auth.Login(loginRequest);
            if (result.Success)
            {
                return Ok(new { Token = result.Data, Message = result.Message });
            }
            else
            {
                if (result.Data == null)
                {
                    // Invalid credentials
                    return Unauthorized(new { Message = result.Message });
                }
                else
                {
                    // Other errors
                    return BadRequestResponse.WithMessage(result.Message);
                }
            }
        }

        [HttpPost("assignRole")]
        public IActionResult AssignRoleToUser(AddUserRole obj)
        {
            var result = _auth.AssignRoleToUser(obj);
            if (result.Success)
            {
                return Ok(new { Message = result.Message });
            }
            else
            {
                return BadRequestResponse.WithMessage(result.Message);
            }
        }

        [HttpPost("addRole")]
        public IActionResult AddRole([FromBody] Role role)
        {
            try
            {
                var addedRole = _auth.AddRole(role);
                return Ok(addedRole);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequestResponse.WithMessage("An error occurred while adding role");
            }
        }
    }
}
