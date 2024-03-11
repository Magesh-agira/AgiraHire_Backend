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

        public User AddUser(User user)
        {
            // Check if the user with the same email already exists, regardless of the isDeleted flag
            var existingUserByEmail = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUserByEmail != null)
            {
                // If the existing user is marked as deleted, we can re-use this record
                // Instead of adding a new entry, we update the existing one
                if (existingUserByEmail.IsDeleted == true)
                {
                    // Update the existing user's properties
                    existingUserByEmail.IsDeleted = false;
                    existingUserByEmail.SetPassword(user.Password); // Optionally update the password if necessary

                    // Save changes to the database
                    _context.SaveChanges();

                    // Return the updated user entity
                    return existingUserByEmail;
                }
                else
                {
                    // Handle the case where a user with the same email already exists and is not marked as deleted
                    throw new Exception("User with the same email already exists.");
                }
            }

            // Check if the user with the same employee ID already exists
            var existingUserByEmployeeId = _context.Users.FirstOrDefault(u => u.Employee_Id == user.Employee_Id);

            if (existingUserByEmployeeId != null)
            {
                // Handle the case where a user with the same employee ID already exists
                throw new Exception("User with the same employee ID already exists.");
            }

            // Set the password for the new user
            user.SetPassword(user.Password);

            // Add the user to the database context
            var addedUser = _context.Users.Add(user);

            // Save changes to the database
            _context.SaveChanges();

            // Return the added user entity
            return addedUser.Entity;
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

        public bool DeleteUser(int UserId)
        {
            var user = _context.Users.Find(UserId);

            if (user == null)
            {
                return false; // User not found
            }

             user.IsDeleted = true; // Set IsDeleted property to true
            _context.SaveChanges();

            return true; // Deletion successful
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
