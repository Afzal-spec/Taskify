using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.DTOs;
using Taskify.Api.Helpers;
using Taskify.Api.Models;
using Taskify.Api.Repositories;

namespace Taskify.Api.Services
{
    public class TaskService
    {
        private readonly ITaskItemRepository repo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor http;


        public TaskService(ITaskItemRepository repo, IMapper mapper, IHttpContextAccessor http)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.http = http;
        }
        private int GetUserId()
        {
            return http.HttpContext.GetUserId();
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            var userId = GetUserId();
            return (await repo.GetAllAsync(userId)).ToList();
        }

        public async Task<TaskItemDto> GetByIdAsync(Guid id)
        {
            var userId = GetUserId();
            var task = await repo.GetByIdAsync(id, userId);
            return task == null ? null : mapper.Map<TaskItemDto>(task);
        }

        public async Task<TaskItem> CreateAsync(CreateTaskItemDto dto)
        {
            var userId = GetUserId();
            var task = mapper.Map<TaskItem>(dto);
            task.UserId = userId;
            return await repo.AddAsync(task);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateTaskItemDto dto)
        {
            var userId = GetUserId();
            var task = await repo.GetByIdAsync(id, userId);
            if (task == null) return false;

            mapper.Map(dto, task);

            await repo.UpdateAsync(task);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var userId = GetUserId();
            var task = await repo.GetByIdAsync(id, userId);
            if (task == null) return false;

            await repo.SoftDeleteAsync(task);
            return true;
        }

        public async Task<TaskItem?> RestoreAsync(Guid id)
        {
            var userId = GetUserId();
            return await repo.RestoreAsync(id, userId);
        }
        public async Task<List<TaskItem>> GetDeletedAsync()
        {
            var userId = GetUserId();
            return (await repo.GetDeletedAsync(userId)).ToList();
        }
    }
}
