using Microsoft.EntityFrameworkCore;
using Slayden.Api.Models;

namespace Slayden.Api.Data
{
    public class SlaydenDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public SlaydenDbContext(DbContextOptions<SlaydenDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<Post>().ToTable("post");
        }
    }
}
