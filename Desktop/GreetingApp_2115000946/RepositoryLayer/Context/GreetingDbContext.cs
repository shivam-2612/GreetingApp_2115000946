using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class GreetingDbContext: DbContext
    {
        public GreetingDbContext(DbContextOptions<GreetingDbContext> options) : base(options) { }

        public DbSet<GreetingEntity> Greetings { get; set; }


    }
}
