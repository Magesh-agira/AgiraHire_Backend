using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AgiraHire_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

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

        public OperationResult<bool> AssignRoleToUser(AddUserRole obj)
        {
            try
            {
                var addRoles = new List<UserRole>();
                var user = _context.Users.SingleOrDefault(s => s.UserId == obj.UserId);
                if (user == null)
                {
                    return new OperationResult<bool>(false, "User is not valid");
                }

                foreach (var role in obj.RoleIds)
                {
                    var userRole = new UserRole
                    {
                        RoleId = role,
                        UserId = user.UserId
                    };
                    addRoles.Add(userRole);
                }

                _context.UserRoles.AddRange(addRoles);
                _context.SaveChanges();
                return new OperationResult<bool>(true, "Roles assigned successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return new OperationResult<bool>(false, "An error occurred while assigning roles");
            }
        }

        public OperationResult<string> Login(LoginRequest loginRequest)
        {
            if (loginRequest != null && !string.IsNullOrWhiteSpace(loginRequest.Email) && !string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                var user = _context.Users.SingleOrDefault(s => s.Email == loginRequest.Email);
                if (user != null)
                {
                    if (user.VerifyPassword(loginRequest.Password))
                    {
                        var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("Email", user.Email)
                };

                        int userIdInt = user.UserId;
                        var userRoles = _context.UserRoles.Where(u => u.UserId == userIdInt).ToList();
                        var roleIds = userRoles.Select(s => s.RoleId).ToList();
                        var roles = _context.Roles.Where(r => roleIds.Contains(r.Id)).ToList();
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.Name));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn);
                        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                        return new OperationResult<string>(jwtToken, "Login successful");
                    }
                    else
                    {
                        return new OperationResult<string>(null, "Incorrect password");
                    }
                }
                else
                {
                    return new OperationResult<string>(null, "Email not found");
                }
            }
            else
            {
                return new OperationResult<string>(null, "Email and password are required");
            }
        }


    }
}
