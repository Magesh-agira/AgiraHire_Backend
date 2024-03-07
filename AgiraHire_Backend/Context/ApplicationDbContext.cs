using AgiraHire_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AgiraHire_Backend.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<opportunity> Opportunities { get; set; }


    }
}
