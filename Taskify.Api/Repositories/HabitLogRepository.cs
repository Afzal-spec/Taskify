using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public class HabitLogRepository : IHabitLogRepository
    {
        private readonly AppDbContext dbContext;

        public HabitLogRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<HabitLog> AddAsync(HabitLog log)
        {
            dbContext.HabitLogs.Add(log);
            await dbContext.SaveChangesAsync();
            return log;
        }

        public async Task<HabitLog?> GetByDateAsync(Guid habitId, DateTime date, int userId)
        {
            return await dbContext.HabitLogs
                .Include(l => l.Habit)
                .FirstOrDefaultAsync(l =>
                    l.HabitId == habitId &&
                    l.Habit.UserId == userId &&
                    l.Date.Date == date.Date
                    );
        }

        public async Task<List<HabitLog>> GetByHabitAsync(Guid habitId, int userId)
        {
            return await dbContext.HabitLogs
                .Include(l => l.Habit)
                .Where(l =>
                    l.HabitId == habitId &&
                    l.Habit.UserId == userId
                )
                .OrderByDescending(l => l.Date)
                .ToListAsync();
        }
        public async Task UpdateAsync(HabitLog log, int userId)
        {
            await dbContext.SaveChangesAsync();
        }
        public async Task<List<HabitLog>> GetByDateAsync(DateTime date, int userId)
        {
            return await dbContext.HabitLogs
                .Include(l => l.Habit)
                .Where(l =>
                    l.Habit.UserId == userId &&
                    l.Date.Date == date.Date
                )
                .ToListAsync();
        }
        public async Task<List<HabitLog>> GetCompletedLogsAsync(Guid habitId, int userId)
        {
            return await dbContext.HabitLogs
                .Include(l => l.Habit)
                .Where(l =>
                    l.HabitId == habitId &&
                    l.Habit.UserId == userId &&
                    l.IsCompleted
                )
                .OrderByDescending(l => l.Date)
                .ToListAsync();
        }
        public async Task<List<DateTime>> GetCompletedDatesAsync(Guid habitId, int userId)
        {
            return await dbContext.HabitLogs
                .Where(l =>
                    l.HabitId == habitId &&
                    l.Habit.UserId == userId &&
                    l.IsCompleted
                )
                .Select(l => l.Date.Date)
                .OrderBy(d => d)
                .ToListAsync();
        }
        public async Task<List<HabitLog>> GetLogsForHabitAsync(Guid habitId)
        {
            return await dbContext.HabitLogs
                .Include(l => l.Habit)
                .Where(l => l.HabitId == habitId && l.IsCompleted)
                .OrderBy(l => l.Date)
                .ToListAsync();
        }




    }
}
