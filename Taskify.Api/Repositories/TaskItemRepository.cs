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
        public async Task<TaskItem?> RestoreAsync(Guid id)
        {
            var task = await dbContext.Tasks
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return null;

            task.IsDeleted = false;
            await dbContext.SaveChangesAsync();
            return task;
        }



        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
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
