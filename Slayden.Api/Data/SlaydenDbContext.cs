using Microsoft.EntityFrameworkCore;
using Slayden.Api.Models;

namespace Slayden.Api.Data
{
    public class SlaydenDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }

        public SlaydenDbContext(DbContextOptions<SlaydenDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<Post>().ToTable("post");

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);
        }
    }
}
