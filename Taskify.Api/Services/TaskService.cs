using AutoMapper;
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
        private readonly IMapper mapper;

        public TaskService(ITaskItemRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
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
            var task = mapper.Map<TaskItem>(dto);

            return await repo.AddAsync(task);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskItemDto dto)
        {
            var task = await repo.GetByIdAsync(id);
            if (task == null) return false;

            mapper.Map(dto, task);

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
