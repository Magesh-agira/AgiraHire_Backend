using AgiraHire_Backend.Context;
using AgiraHire_Backend.Interfaces;
using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
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
                    existingUserByEmail.SetPassword(user.Password); // Optionally update the password if necessary
                    existingUserByEmail.Employee_Id = user.Employee_Id; // Update Employee ID

                    // Save changes to the database
                    _context.SaveChanges();

                    // Return the updated user entity
                    return existingUserByEmail;
                }
                else
                {
                    // Add error message to ModelState instead of throwing exception
                    ModelState.AddModelError("Email", "User with the same email already exists.");
                    return null;
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
            return addedUser.Entity;
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

        public List<User> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        // Add ModelState property for error messages
        public ModelStateDictionary ModelState { get; set; } = new ModelStateDictionary();
    }
}
