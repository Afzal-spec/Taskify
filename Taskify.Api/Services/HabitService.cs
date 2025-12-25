using AutoMapper;
using Taskify.Api.DTOs.HabitsDtos;
using Taskify.Api.Helpers;
using Taskify.Api.Models;
using Taskify.Api.Repositories;

namespace Taskify.Api.Services
{
    public class HabitService
    {
        private readonly IHabitRepository repo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor http;

        public HabitService(
            IHabitRepository repo,
            IMapper mapper,
            IHttpContextAccessor http)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.http = http;
        }

        private int GetUserId() => http.HttpContext.GetUserId();

        public async Task<List<HabitDto>> GetAllAsync()
        {
            var userId = GetUserId();
            var habits = await repo.GetAllAsync(userId);
            return mapper.Map<List<HabitDto>>(habits);
        }

        public async Task<HabitDto?> GetByIdAsync(Guid id)
        {
            var userId = GetUserId();
            var habit = await repo.GetByIdAsync(id, userId);
            return habit == null ? null : mapper.Map<HabitDto>(habit);
        }

        public async Task<HabitDto> CreateAsync(CreateHabitDto dto)
        {
            var habit = mapper.Map<Habit>(dto);
            habit.UserId = GetUserId();
            habit.CreatedAt = DateTime.UtcNow;

            await repo.AddAsync(habit);
            return mapper.Map<HabitDto>(habit);
        }

        public async Task<bool> ArchiveAsync(Guid id)
        {
            var userId = GetUserId();
            var habit = await repo.GetByIdAsync(id, userId);

            if (habit == null) return false;

            habit.IsArchived = true;
            await repo.UpdateAsync(habit);
            return true;
        }
    }
}
