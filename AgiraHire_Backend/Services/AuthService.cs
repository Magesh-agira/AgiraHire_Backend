using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using Microsoft.EntityFrameworkCore;
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

        public OperationResult<Role> AddRole(Role role)
        {
            try
            {
                if (role == null)
                {
                    return new OperationResult<Role>(null, "Role object cannot be null", 400);
                }

                if (string.IsNullOrWhiteSpace(role.Name))
                {
                    return new OperationResult<Role>(null, "Role Name is required", 400);
                }

                if (string.IsNullOrWhiteSpace(role.Description))
                {
                    return new OperationResult<Role>(null, "Role Description is required", 400);
                }

                var existingRole = _context.Roles.FirstOrDefault(r => r.Name == role.Name);
                if (existingRole != null)
                {
                    return new OperationResult<Role>(null, "Role with the same name already exists", 400);
                }

                var addedRole = _context.Roles.Add(role);
                _context.SaveChanges();

                // Return the added role object along with the success message
                return new OperationResult<Role>(addedRole.Entity, "Role Added successfully", 200);
            }
            catch (Exception ex)
            {
                return new OperationResult<Role>(null, "Role cannot be added", 500);
            }
        }


        public OperationResult<bool> AssignRoleToUser(AddUserRole obj)
        {
            if (obj.UserId <= 0)
            {
                return new OperationResult<bool>(false, "A valid User ID is required.", 400);
            }

            if (obj.RoleIds == null || !obj.RoleIds.Any())
            {
                return new OperationResult<bool>(false, "At least one Role ID is required.", 400);
            }

            try
            {
     
                var user = _context.Users.Find(obj.UserId);
                if (user == null)
                {
                    return new OperationResult<bool>(false, "User does not exist.", 404);
                }

                var addRoles = new List<UserRole>();
                foreach (var roleId in obj.RoleIds)
                {
                   
                    var roleExists = _context.Roles.Any(r => r.Id == roleId);
                    if (!roleExists)
                    {
                     
                        return new OperationResult<bool>(false, $"Role ID {roleId} is invalid.", 400);
                    }

                
                    addRoles.Add(new UserRole { RoleId = roleId, UserId = user.UserId });
                }

                _context.UserRoles.AddRange(addRoles);
                _context.SaveChanges();
                return new OperationResult<bool>(true, "Roles assigned successfully.",200);
            }
            catch (Exception ex)
            { 
                return new OperationResult<bool>(false, $"An error occurred while assigning roles: {ex.Message}", 500);
            }
        }

        public OperationResult<List<Role>> GetRoles()
        {
            var roles = _context.Roles.ToList();
            if (roles != null)
            {
                return new OperationResult<List<Role>>(roles, "Roles retrieved successfully",200);
            }
            else
            {
                return new OperationResult<List<Role>>(null, "No roles found", 404);
            }
        }

        public OperationResult<List<UserRole>> GetUserRoles()
        {
            try
            {
                var userRoles = _context.UserRoles.ToList();
                return new OperationResult<List<UserRole>>(userRoles, "User roles retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return new OperationResult<List<UserRole>>(null, $"An error occurred: {ex.Message}", 500);
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
                            expires: DateTime.UtcNow.AddMinutes(60),
                            signingCredentials: signIn);
                        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                        return new OperationResult<string>(jwtToken, "Login successful",200);
                    }
                    else
                    {
                        // Incorrect password error
                        return new OperationResult<string>(loginRequest.Password, "Incorrect password", 401);
                    }
                }
                else
                {
                    // Email not found error
                    return new OperationResult<string>(loginRequest.Email, "Email not found", 404);
                }
            }
            else
            {
                // Email and password are required error
                return new OperationResult<string>(null, "Email and password are required", 400);
            }
        }
    }
}
