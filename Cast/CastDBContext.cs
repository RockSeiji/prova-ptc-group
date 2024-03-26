using Cast.Models;
using Microsoft.EntityFrameworkCore;

namespace Cast
{
    public partial class CastDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public CastDBContext(DbContextOptions<CastDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(l => l.Username);
            modelBuilder.Entity<User>().Property(l => l.Password).IsRequired();
            modelBuilder.Entity<User>().Property(l => l.Role).IsRequired();

            modelBuilder.Entity<Post>().HasKey(l => l.Id);
            modelBuilder.Entity<Post>().Property(l => l.Title).IsRequired();
            modelBuilder.Entity<Post>().Property(l => l.Descriptioon).IsRequired();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
