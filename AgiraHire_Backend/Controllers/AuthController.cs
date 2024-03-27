using Microsoft.AspNetCore.Mvc;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using AgiraHire_Backend.Response.AgiraHire_Backend.Responses;
using System;

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
                return Ok(new {result.Data, StatusCode = 200, Message = result.Message });
            }
            else
            {
                if (result.ErrorCode == 401)
                {
                    // Unauthorized (401) error
                    return Ok(new { StatusCode = 401, Message = result.Message });
                }
                else if (result.ErrorCode == 404)
                {
                    // Not Found (404) error
                    return Ok(new { StatusCode = 404, Message = result.Message });
                }
                else if (result.ErrorCode == 400)
                {
                    // Bad Request (400) error
                    return Ok(new { StatusCode = 400, Message = result.Message });
                }
                else
                {
                    // Other errors
                    return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
                }
            }
        }

        [HttpPost("assignRole")]
        public IActionResult AssignRoleToUser(AddUserRole obj)
        {
            try
            {
                var result = _auth.AssignRoleToUser(obj);
                if (result.Success)
                {
                    return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
                }
                else
                {
                    return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while assigning role to user" });
            }
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _auth.GetRoles(); // Implement this method in your IRoleService and RoleService
                return Ok(roles);
            }
            catch (Exception ex)
            {
                // Log the exception
                return Ok(new { StatusCode = 500, Message = "An error occurred while fetching roles." });

            }
        }


        [HttpPost("addRole")]
        public IActionResult AddRole([FromBody] Role role)
        {   
            try
            {
                var addedRole = _auth.AddRole(role);
                return Ok(new { StatusCode = 200, Message = "Role added successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest(new { StatusCode = 400, Message = "An error occurred while adding role" });
            }
        }
    }
}
