using CodingChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Contexts
{
    public class CodingTaskContext : DbContext
    {
        public CodingTaskContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Taskk>? Taskks { get; set; }

        public DbSet<User>? Users { get; set; }

    }
}
