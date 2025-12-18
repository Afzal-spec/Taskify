using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.Models;

namespace Taskify.Api.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext dbContext;

        public NoteRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Note> AddAsync(Note note)
        {
            dbContext.Notes.Add(note);
            await dbContext.SaveChangesAsync();
            return note;
        }

        public async Task<IEnumerable<Note>> GetAllAsync(int userId)
        {
            return await dbContext.Notes.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<Note?> GetByIdAsync(Guid id, int userId)
        {
            return await dbContext.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        }

        public async Task SoftDeleteAsync(Note note)
        {
            note.IsDeleted = true;
            await dbContext.SaveChangesAsync();
        }

        public async Task<Note> UpdateAsync(Note note)
        {
            dbContext.Notes.Update(note);
            await dbContext.SaveChangesAsync();
            return note;
        }
    }
}
