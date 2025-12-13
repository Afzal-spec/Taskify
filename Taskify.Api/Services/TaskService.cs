using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.DTOs;
using Taskify.Api.Models;
using Taskify.Api.Repositories;

namespace Taskify.Api.Services
{
    public class TaskService
    {
        private readonly ITaskItemRepository repo;

        public TaskService(ITaskItemRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return (await repo.GetAllAsync()).ToList();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<TaskItem> CreateAsync(CreateTaskItemDto dto)
        {
            var task = new TaskItem
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
            };
            return await repo.AddAsync(task);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskItemDto dto)
        {
            var task = await repo.GetByIdAsync(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;
            task.DueDate = dto.DueDate;
            task.Priority = dto.Priority;

            await repo.UpdateAsync(task);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await repo.GetByIdAsync(id);
            if (task == null) return false;

            await repo.DeleteAsync(task);
            return true;
        }
    }
}
