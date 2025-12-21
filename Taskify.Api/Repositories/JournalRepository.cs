using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public class JournalRepository : IJournalRepository
    {
        private readonly AppDbContext dbContext;

        public JournalRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<JournalEntry> AddAsync(JournalEntry entry)
        {
            dbContext.JournalEntries.Add(entry);
            await dbContext.SaveChangesAsync();
            return entry;
        }

        public async Task<IEnumerable<JournalEntry>> GetAllAsync(int userId)
        {
            return await dbContext.JournalEntries
                .Where(j => j.UserId == userId)
                .OrderByDescending(j => j.Date)
                .ToListAsync();
        }

        public async Task<JournalEntry?> GetByDateAsync(DateTime date, int userId)
        {
            return await dbContext.JournalEntries
                .FirstOrDefaultAsync(j => j.Date.Date == date.Date && j.UserId == userId);
        }

        public async Task SoftDeleteAsync(JournalEntry entry)
        {
            entry.IsDeleted = true;
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(JournalEntry entry)
        {
            entry.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }
}
