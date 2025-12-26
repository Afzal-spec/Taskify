using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public interface IHabitLogRepository
    {
        Task<HabitLog?> GetByDateAsync(Guid habitId, DateTime date, int userId);
        Task<HabitLog> AddAsync(HabitLog log);
        Task<List<HabitLog>> GetByHabitAsync(Guid habitId, int userId);
        Task UpdateAsync(HabitLog log, int userId);
        Task<List<HabitLog>> GetByDateAsync(DateTime date, int userId);

    }
}
