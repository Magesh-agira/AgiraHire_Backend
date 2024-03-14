using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgiraHire_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration ;
        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {

            _context = context;
            _configuration = configuration;

        }

        public Role AddRole(Role role)
        {
            var addedRole = _context.Roles.Add(role);
            _context.SaveChanges();
            return addedRole.Entity;
        }







        public bool AssignRoleToUser(AddUserRole obj)
        {
            try
            {
                var addRoles = new List<UserRole>();
                var user = _context.Users.SingleOrDefault(s => s.UserId == obj.UserId);
                if (user == null)
                {
                    throw new Exception("User is not valid");
                }
                foreach (var role in obj.RoleIds)
                {
                    var userRole = new UserRole();
                    userRole.RoleId = role;
                    userRole.UserId = user.UserId;
                    addRoles.Add(userRole);


                }
                _context.UserRoles.AddRange(addRoles);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


        }

 


        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest.Email != null && loginRequest.Password !=null)
            {
                var user = _context.Users.SingleOrDefault(s => s.Email == loginRequest.Email );
                if (user != null && user.VerifyPassword(loginRequest.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                        new Claim("Id", user.UserId.ToString()),
                        new Claim("Email",user.Email)
                    };
                    int userIdInt = user.UserId;
                    var userRoles = _context.UserRoles.Where(u=> u.UserId== userIdInt).ToList();
                    var roleIds = userRoles.Select(s => s.RoleId).ToList();
                    var roles = _context.Roles.Where(r=> roleIds.Contains(r.Id)).ToList();
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
                    var signIn=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    
                    return jwtToken;



                }
                else
                {
                    throw new Exception("user is not valid");
                }
            }
            else
            {
                throw new Exception("Credentials are not valid ");
            }
        }
    }
}
