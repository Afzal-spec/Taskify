using Microsoft.EntityFrameworkCore;
using Taskify.Api.Models;
namespace Taskify.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Soft delete global filter
            modelBuilder.Entity<TaskItem>()
                .HasQueryFilter(t => !t.IsDeleted);

            // 🔥 USER → TASK 1:N RELATIONSHIP
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}
