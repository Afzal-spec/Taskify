using AutoMapper;
using Taskify.Api.DTOs.HabitsDtos;
using Taskify.Api.Helpers;
using Taskify.Api.Models;
using Taskify.Api.Repositories;

namespace Taskify.Api.Services
{
    public class HabitLogService
    {
        private readonly IHabitLogRepository repo;
        private readonly IHabitRepository habitRepo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor http;

        public HabitLogService(IHabitLogRepository repo, IHabitRepository habitRepo, IMapper mapper, IHttpContextAccessor http)
        {
            this.repo = repo;
            this.habitRepo = habitRepo;
            this.mapper = mapper;
            this.http = http;
        }
        private int GetUserId() => http.HttpContext!.GetUserId();
        public async Task<HabitLogDto> CreateAsync(CreateHabitLogDto dto)
        {
            var userId = GetUserId();
            var habit = await habitRepo.GetByIdAsync(dto.HabitId, userId);
            if (habit == null)
                throw new Exception("Habit not found");
            var existing = await repo.GetByDateAsync(dto.HabitId, dto.Date.Date, userId);
            if (existing != null)
                throw new Exception("Log already exists for this date");
            var log = mapper.Map<HabitLog>(dto);
            log.HabitId = dto.HabitId;
            await repo.AddAsync(log);
            return mapper.Map<HabitLogDto>(log);
        }

        public async Task<List<HabitLogDtoWithHabit>> GetByHabitAsync(Guid habitId)
        {
            var userId = GetUserId();

            // 🔐 Validate habit ownership
            var habit = await habitRepo.GetByIdAsync(habitId, userId);
            if (habit == null)
                throw new Exception("Habit not found");

            var logs = await repo.GetByHabitAsync(habitId, userId);
            return mapper.Map<List<HabitLogDtoWithHabit>>(logs);
        }
        public async Task<HabitLogDto> MarkCompletedAsync(MarkHabitCompletedDto dto)
        {
            var userId = GetUserId();

            var habit = await habitRepo.GetByIdAsync(dto.HabitId, userId);
            if (habit == null)
                throw new Exception("Habit not found");

            var log = await repo.GetByDateAsync(dto.HabitId, dto.Date.Date, userId);

            if (log == null)
            {
                log = new HabitLog
                {
                    HabitId = dto.HabitId,
                    Date = dto.Date.Date,
                    IsCompleted = dto.IsCompleted
                };

                await repo.AddAsync(log);
            }
            else
            {
                log.IsCompleted = dto.IsCompleted;
                await repo.UpdateAsync(log, userId);
            }

            return mapper.Map<HabitLogDto>(log);
        }
        public async Task<List<DailyHabitStatusDto>> GetDailyStatusAsync(DateTime date)
        {
            var userId = GetUserId();

            // 1. Get all active habits for user
            var habits = await habitRepo.GetAllAsync(userId);

            // 2. Get all logs for that date
            var logs = await repo.GetByDateAsync(date.Date, userId);

            // 3. Map habits + logs
            var result = habits.Select(h =>
            {
                var log = logs.FirstOrDefault(l => l.HabitId == h.Id);

                return new DailyHabitStatusDto
                {
                    HabitId = h.Id,
                    Name = h.Name,
                    Description = h.Description,
                    Frequency = h.Frequency,
                    Date = date.Date,
                    IsCompleted = log != null && log.IsCompleted
                };
            }).ToList();

            return result;
        }
        public async Task<HabitStreakDto> GetCurrentStreakAsync(Guid habitId)
        {
            var userId = GetUserId();

            // 🔐 Validate habit ownership
            var habit = await habitRepo.GetByIdAsync(habitId, userId);
            if (habit == null)
                throw new Exception("Habit not found");

            var logs = await repo.GetCompletedLogsAsync(habitId, userId);

            if (!logs.Any())
            {
                return new HabitStreakDto
                {
                    HabitId = habit.Id,
                    HabitName = habit.Name,
                    Frequency = habit.Frequency,
                    CurrentStreak = 0
                };
            }

            var today = DateTime.UtcNow.Date;
            var streak = 0;
            var expectedDate = today;

            foreach (var log in logs)
            {
                if (log.Date.Date == expectedDate)
                {
                    streak++;
                    expectedDate = expectedDate.AddDays(-1);
                }
                else if (log.Date.Date < expectedDate)
                {
                    break; // ❌ gap → streak ends
                }
            }

            return new HabitStreakDto
            {
                HabitId = habit.Id,
                HabitName = habit.Name,
                Frequency = habit.Frequency,
                CurrentStreak = streak
            };
        }

        public async Task<HabitStreakDto> GetStreakAsync(Guid habitId)
        {
            var userId = GetUserId();

            var habit = await habitRepo.GetByIdAsync(habitId, userId);
            if (habit == null)
                throw new Exception("Habit not found");

            var logs = await repo.GetByHabitAsync(habitId, userId);

            if (!logs.Any())
                return mapper.Map<HabitStreakDto>((habit, 0, 0));

            var dates = logs
                .Where(l => l.IsCompleted)
                .Select(l => l.Date.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            // ---------- Longest Streak ----------
            int longestStreak = 1;
            int tempStreak = 1;

            for (int i = 1; i < dates.Count; i++)
            {
                if (dates[i] == dates[i - 1].AddDays(1))
                    tempStreak++;
                else
                    tempStreak = 1;

                longestStreak = Math.Max(longestStreak, tempStreak);
            }

            // ---------- Current Streak ----------
            int currentStreak = 0;
            var lookup = new HashSet<DateTime>(dates);

            DateTime day = DateTime.UtcNow.Date;

            while (lookup.Contains(day))
            {
                currentStreak++;
                day = day.AddDays(-1);
            }

            return mapper.Map<HabitStreakDto>((habit, currentStreak, longestStreak));
        }




    }
}
