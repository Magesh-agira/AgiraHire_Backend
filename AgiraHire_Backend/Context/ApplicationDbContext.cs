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
        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<Interview_round> Interview_Rounds {  get; set; }    
        
        public DbSet<InterviewSlot> InterviewSlots { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Applicant>()
                .HasOne<opportunity>()
                .WithMany()
                .HasForeignKey(a => a.OpportunityId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InterviewSlot>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(b => b.InterviewerId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InterviewSlot>()
                .HasOne<Interview_round>()     // Assuming Interview is the name of the related entity
                .WithMany()              // Assuming multiple InterviewSlots can belong to the same Interview
                .HasForeignKey(i => i.RoundId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Feedback>()
                .HasOne<Applicant>()
                .WithMany()
                .HasForeignKey(a => a.ApplicantId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u=> u.InterviewerId).IsRequired().OnDelete(DeleteBehavior.Restrict); 

        }

    }
}
