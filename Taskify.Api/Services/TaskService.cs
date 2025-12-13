using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.DTOs;
using Taskify.Api.Models;

namespace Taskify.Api.Services
{
    public class TaskService
    {
        private readonly AppDbContext dbContext;

        public TaskService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await dbContext.Tasks.FindAsync(id);
        }

        public async Task<TaskItem> CreateAsync(CreateTaskItemDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
            };

            await dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskItemDto dto)
        {
            var task = await dbContext.Tasks.FindAsync(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;
            task.DueDate = dto.DueDate;
            task.Priority = dto.Priority;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await dbContext.Tasks.FindAsync(id);
            if (task == null) return false;

            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
