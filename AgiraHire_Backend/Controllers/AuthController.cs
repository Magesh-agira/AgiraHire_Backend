using Microsoft.AspNetCore.Mvc;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using AgiraHire_Backend.Response.AgiraHire_Backend.Responses;
using System;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace AgiraHire_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Admin")]

    public class AuthController : ControllerBase
    {   
        private readonly IAuthService _auth;
        public AuthController(IAuthService  auth)
        {
            _auth = auth;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var result = _auth.Login(loginRequest);
                return Ok(new { result.Data, StatusCode = result.ErrorCode, Message = result.Message });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An unexpected error occurred");
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
                return Ok( new { StatusCode = 500, Message = "An error occurred while assigning role to user" });
            }
        }

        [HttpGet("getroles")]
        public IActionResult GetRoles()
        {
            try
            {
                var result = _auth.GetRoles(); // Implement this method in your IRoleService and RoleService
                return Ok(new {result.Data, StatusCode = result.ErrorCode, Message = result.Message });
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
            
                var result = _auth.AddRole(role);

                if (result.Success)
                {
                   
                    return Ok(new {result.Data, StatusCode = result.ErrorCode, Message = result.Message });
                }
                else
                {
                
                    return Ok(new { StatusCode = result.ErrorCode, Message = result.Message });
                }
            }
            catch (Exception ex)
            {
               
                return Ok(new { StatusCode = 400, Message = "An error occurred while adding role" });
            }
        }

        [HttpGet("userRoles")]
        public IActionResult GetUserRoles()
        {
            try
            {
                var result = _auth.GetUserRoles();
                return StatusCode(result.ErrorCode, new { Data = result.Data, Message = result.Message });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, new { Message = "An unexpected error occurred" });
            }
        }

    }
}
