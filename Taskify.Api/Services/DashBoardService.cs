using Taskify.Api.DTOs.DashBoardDtos;
using Taskify.Api.Helpers;
using Taskify.Api.Repositories;

namespace Taskify.Api.Services
{
    public class DashBoardService
    {
        private readonly IHabitRepository habitRepo;
        private readonly IHabitLogRepository habitLogRepo;
        private readonly IHttpContextAccessor http;
        private readonly ITaskItemRepository taskRepo;

        public DashBoardService(IHabitRepository habitRepo,
            IHabitLogRepository habitLogRepo,
            IHttpContextAccessor http,
            ITaskItemRepository taskRepo)
        {
            this.habitRepo = habitRepo;
            this.habitLogRepo = habitLogRepo;
            this.http = http;
            this.taskRepo = taskRepo;
        }

        private int GetUserId() => http.HttpContext!.GetUserId();
        public async Task<List<TodayHabitStatusDto>> GetTodayStatusAsync()
        {
            var userId = GetUserId();
            var today = DateTime.UtcNow.Date;

            var habits = await habitRepo.GetAllAsync(userId);
            var logs = await habitLogRepo.GetByDateAsync(today, userId);

            var logLookup = logs.ToDictionary(l => l.HabitId, l => l.IsCompleted);

            return habits.Select(h => new TodayHabitStatusDto
            {
                HabitId = h.Id,
                Name = h.Name,
                Description = h.Description,
                Frequency = h.Frequency,
                IsCompletedToday = logLookup.ContainsKey(h.Id) && logLookup[h.Id]
            }).ToList();
        }

        public async Task<HabitStreakSummaryDto> GetHabitStreakSummaryAsync()
        {
            var userId = GetUserId();
            var today = DateTime.UtcNow.Date;

            var habits = await habitRepo.GetAllAsync(userId);
            var todayLogs = await habitLogRepo.GetByDateAsync(today, userId);

            // 1️⃣ Total habits
            int totalHabits = habits.Count;

            // 2️⃣ Completed today
            int completedToday = todayLogs.Count(l => l.IsCompleted);

            BestStreakDto? bestStreak = null;
            var activeStreaks = new List<ActiveStreakDto>();

            foreach (var habit in habits)
            {
                var logs = await habitLogRepo.GetByHabitAsync(habit.Id, userId);

                if (!logs.Any())
                    continue;

                var dates = logs
                    .Where(l => l.IsCompleted)
                    .Select(l => l.Date.Date)
                    .Distinct()
                    .OrderBy(d => d)
                    .ToList();

                if (!dates.Any())
                    continue;

                // 🔥 Longest streak
                int longest = 1, temp = 1;
                for (int i = 1; i < dates.Count; i++)
                {
                    if (dates[i] == dates[i - 1].AddDays(1))
                        temp++;
                    else
                        temp = 1;

                    longest = Math.Max(longest, temp);
                }

                // 🔥 Current streak (from today backwards)
                int current = 0;
                DateTime cursor = today;

                for (int i = dates.Count - 1; i >= 0; i--)
                {
                    if (dates[i] == cursor)
                    {
                        current++;
                        cursor = cursor.AddDays(-1);
                    }
                    else
                        break;
                }

                // Best streak (GLOBAL)
                if (bestStreak == null || longest > bestStreak.Streak)
                {
                    bestStreak = new BestStreakDto
                    {
                        HabitId = habit.Id,
                        Name = habit.Name,
                        Streak = longest
                    };
                }

                // Active streaks (ONLY if ongoing)
                if (current > 0)
                {
                    activeStreaks.Add(new ActiveStreakDto
                    {
                        HabitId = habit.Id,
                        Name = habit.Name,
                        CurrentStreak = current
                    });
                }
            }

            return new HabitStreakSummaryDto
            {
                TotalHabits = totalHabits,
                CompletedToday = completedToday,
                BestStreak = bestStreak,
                ActiveStreaks = activeStreaks
            };
        }

        public async Task<TaskSummaryDto> GetTaskSummaryAsync()
        {
            var userId = GetUserId();
            var tasks = await taskRepo.GetAllAsync(userId);

            var total = tasks.Count();
            var completed = tasks.Count(t => t.IsCompleted);
            var overdue = tasks.Count(t =>
                !t.IsCompleted &&
                t.DueDate != null &&
                t.DueDate.Value.Date < DateTime.UtcNow.Date
            );

            var pending = total - completed;
            var completionRate = total == 0
                ? 0
                : Math.Round((double)completed / total * 100, 2);

            return new TaskSummaryDto
            {
                Total = total,
                Completed = completed,
                Pending = pending,
                Overdue = overdue,
                CompletionRate = completionRate
            };
        }

        public async Task<TodayTasksSummaryDto> GetTodayTasksAsync()
        {
            var userId = GetUserId();
            var today = DateTime.UtcNow.Date;

            var tasks = await taskRepo.GetAllAsync(userId);

            var todayTasks = tasks.Where(t =>
                (t.DueDate != null && t.DueDate.Value.Date == today) ||
                (t.DueDate != null && t.DueDate.Value.Date < today && !t.IsCompleted)
            ).ToList();

            return new TodayTasksSummaryDto
            {
                TotalTasksToday = todayTasks.Count,
                CompletedToday = todayTasks.Count(t => t.IsCompleted),
                PendingToday = todayTasks.Count(t => !t.IsCompleted && t.DueDate?.Date == today),
                OverdueToday = todayTasks.Count(t => !t.IsCompleted && t.DueDate < today),
                Tasks = todayTasks.Select(t => new TodayTaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    Priority = t.Priority
                }).ToList()
            };
        }



    }
}
