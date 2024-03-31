using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AgiraHire_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public OperationResult<User> AddUser(User user)
        {
            // Check if user object is null
            if (user == null)
            {
                return new OperationResult<User>(null, "User object cannot be null", 400);
            }

            // Check if email is provided
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return new OperationResult<User>(null, "Email is required", 400);
            }

            // Check if password is provided
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return new OperationResult<User>(null, "Password is required", 400);
            }

            // Check if employee ID is provided
            if (string.IsNullOrWhiteSpace(user.Employee_Id))
            {
                return new OperationResult<User>(null, "Employee ID is required", 400);
            }

            // Check if the user with the same email already exists, regardless of the isDeleted flag
            var existingUserByEmail = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUserByEmail != null)
            {
                if (existingUserByEmail.IsDeleted == true)
                {
                    existingUserByEmail.SetPassword(user.Password);
                    existingUserByEmail.Employee_Id = user.Employee_Id;
                    _context.SaveChanges();
                    return new OperationResult<User>(existingUserByEmail, "User updated successfully", 200);
                }
                else
                {
                    return new OperationResult<User>(null, "User with the same email already exists.", 400);
                }
            }

            // Set the password for the new user
            user.SetPassword(user.Password);
            user.IsDeleted = false;

            // Add the user to the database context
            var addedUser = _context.Users.Add(user);

            // Save changes to the database
            _context.SaveChanges();

            // Return the added user entity
            return new OperationResult<User>(addedUser.Entity, "User added successfully", 200);
        }


        public OperationResult<bool> DeleteUser(int UserId)
        {
            try
            {
                // Check if UserId is valid
                if (UserId <= 0)
                {
                    return new OperationResult<bool>(false, "Invalid UserId", 400);
                }

                var user = _context.Users.Find(UserId);

                if (user == null)
                {
                    return new OperationResult<bool>(false, "User not found", 404);
                }

                // Soft delete the user by setting IsDeleted flag to true
                user.IsDeleted = true;
                _context.SaveChanges();

                return new OperationResult<bool>(true, "User deleted successfully", 200);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new OperationResult<bool>(false, $"An error occurred while deleting user: {ex.Message}", 500);
            }
        }

        public OperationResult<List<User>> GetUsers()
        {
            var users = _context.Users.ToList();

            if (users != null && users.Any())
            {
                return new OperationResult<List<User>>(users, "Users retrieved successfully", 200);
            }
            else
            {
                return new OperationResult<List<User>>(null, "No users found", 404);
            }
        }

        //public ModelStateDictionary ModelState { get; set; } = new ModelStateDictionary();
    }
}
