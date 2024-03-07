using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
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
        public string Login([FromBody] LoginRequest obj)
        {
            var token = _auth.Login(obj);
            return token;
        }

        // GET api/<AuthController>/5
        [HttpPost("assignRole")]
        public bool AssignRoleToUser([FromBody] AddUserRole userRole)
        {
            var addedUserRole =_auth.AssignRoleToUser(userRole);
            return addedUserRole;
        }

        // POST api/<AuthController>
        [HttpPost("addUser")]
        public User AddUser([FromBody] User user)
        {
            var addeduser = _auth.AddUser(user);
            return addeduser;

        }

        // PUT api/<AuthController>/5
        [HttpPost("addRole")]
        public Role AddRole( [FromBody] Role role)
        {
            var addRole=_auth.AddRole(role);
            return addRole;
        }

    }
}
