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

        //public async Task DeleteAsync(TaskItem task)
        //{
        //    dbContext.Tasks.Remove(task);
        //    await dbContext.SaveChangesAsync();
        //}
        public async Task SoftDeleteAsync(TaskItem task)
        {
            task.IsDeleted = true;
            await dbContext.SaveChangesAsync();
        }
        public async Task<TaskItem?> RestoreAsync(Guid id, int userId)
        {
            var task = await dbContext.Tasks
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return null;

            task.IsDeleted = false;
            await dbContext.SaveChangesAsync();
            return task;
        }



        public async Task<IEnumerable<TaskItem>> GetAllAsync(int userId)
        {
            return await dbContext.Tasks
            .Where(t => t.UserId == userId)
            .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id, int userId)
        {
            return await dbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
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
