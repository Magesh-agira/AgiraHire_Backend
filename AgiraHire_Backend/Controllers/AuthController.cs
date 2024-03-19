using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<AuthController>
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
                    return BadRequest(new { Message = result.Message });
                }

            }
        }

        // GET api/<AuthController>/5
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
                return BadRequest(new { Message = result.Message });
            }
        }

        // POST api/<AuthController>


        // PUT api/<AuthController>/5
        [HttpPost("addRole")]
        public Role AddRole( [FromBody] Role role)
        {
            var addRole=_auth.AddRole(role);
            return addRole;
        }




    }
}
