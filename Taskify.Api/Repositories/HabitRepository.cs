using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public class HabitRepository : IHabitRepository
    {
        private readonly AppDbContext dbContext;

        public HabitRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Habit> AddAsync(Habit habit)
        {
            dbContext.Habits.Add(habit);
            await dbContext.SaveChangesAsync();
            return habit;
        }

        public async Task<List<Habit>> GetAllAsync(int userId)
        {
            return await dbContext.Habits
                .Where(h  => h.UserId == userId && !h.IsArchived)
                .ToListAsync();
        }

        public async Task<Habit?> GetByIdAsync(Guid id, int userId)
        {
            return await dbContext.Habits.FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);
        }

        public async Task SoftDeleteAsync(Habit habit)
        {
            habit.IsArchived = true;
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Habit habit)
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
