using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext dbContext;

        public TaskItemRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            dbContext.Tasks.AddAsync(task);
            await dbContext.SaveChangesAsync();
            return task;
        }

        public async Task DeleteAsync(TaskItem task)
        {
            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await dbContext.Tasks.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            dbContext.Tasks.Update(task);
            await dbContext.SaveChangesAsync();
        }
    }
}
