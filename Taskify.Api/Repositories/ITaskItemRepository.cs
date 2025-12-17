using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<TaskItem> AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        //Task DeleteAsync(TaskItem task);
        Task SoftDeleteAsync(TaskItem task);
        Task<TaskItem?> RestoreAsync(Guid id);
        Task SaveChangesAsync();
    }
}
