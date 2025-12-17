using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync(int userId);
        Task<TaskItem?> GetByIdAsync(Guid id, int userId);
        Task<TaskItem> AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        //Task DeleteAsync(TaskItem task);
        Task SoftDeleteAsync(TaskItem task);
        Task<TaskItem?> RestoreAsync(Guid id, int userId);
        Task SaveChangesAsync();
    }
}
