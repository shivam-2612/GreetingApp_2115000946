using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;

namespace RepositoryLayer.Context
{
    public class GreetingDbContext : DbContext
    {
        public GreetingDbContext(DbContextOptions<GreetingDbContext> options) : base(options) { }

        public DbSet<GreetingEntity> Greetings { get; set; }
        public DbSet<UserEntity> Users { get; set; } // ✅ Add Users Table


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GreetingEntity>()
                .HasOne(g => g.User)
                .WithMany(u => u.Greetings)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
