using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public interface IHabitRepository
    {
        Task<Habit> AddAsync(Habit habit);
        Task<List<Habit>> GetAllAsync(int userId);
        Task<Habit?> GetByIdAsync(Guid id, int userId);
        Task UpdateAsync(Habit habit);
        Task SoftDeleteAsync(Habit habit);
    }
}
