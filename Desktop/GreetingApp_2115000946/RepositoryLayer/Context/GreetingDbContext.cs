using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;

namespace RepositoryLayer.Context
{
    public class GreetingDbContext : DbContext
    {
        public GreetingDbContext(DbContextOptions<GreetingDbContext> options) : base(options) { }

        public DbSet<GreetingEntity> Greetings { get; set; }
        public DbSet<UserModel> Users { get; set; } // ✅ Add Users Table
    }
}
