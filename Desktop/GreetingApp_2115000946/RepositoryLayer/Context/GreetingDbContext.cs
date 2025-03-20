using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;

namespace RepositoryLayer.Context
{
    public class GreetingDbContext : DbContext
    {
        public GreetingDbContext(DbContextOptions<GreetingDbContext> options) : base(options) { }

        public DbSet<GreetingEntity> Greetings { get; set; }
<<<<<<< Updated upstream
        public DbSet<UserModel> Users { get; set; } // ✅ Add Users Table
=======
        public DbSet<UserEntity> Users { get; set; } // ✅ Add Users Table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GreetingEntity>()
                .HasOne(g => g.User) // Greeting has one User
                .WithMany() // User can have many Greetings
                .HasForeignKey(g => g.UserId) // Foreign Key
                .OnDelete(DeleteBehavior.Cascade);
        }
>>>>>>> Stashed changes
    }
}
