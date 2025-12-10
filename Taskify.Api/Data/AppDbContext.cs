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
    }
}
