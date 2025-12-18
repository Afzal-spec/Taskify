using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllAsync(int userId);
        Task<Note?> GetByIdAsync(Guid id, int userId);
        Task<Note> AddAsync(Note note);
        Task<Note> UpdateAsync(Note note);
        Task SoftDeleteAsync(Note note);
    }
}
