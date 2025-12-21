using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public interface IJournalRepository
    {
        Task<JournalEntry?> GetByDateAsync(DateTime date, int userId);
        Task<IEnumerable<JournalEntry>> GetAllAsync(int userId);
        Task<JournalEntry> AddAsync(JournalEntry entry);
        Task UpdateAsync(JournalEntry entry);
        Task SoftDeleteAsync(JournalEntry entry);
    }
}
