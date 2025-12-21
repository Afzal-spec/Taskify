using AutoMapper;
using Taskify.Api.DTOs.NotesDtos;
using Taskify.Api.Helpers;
using Taskify.Api.Models;
using Taskify.Api.Repositories;

namespace Taskify.Api.Services
{
    public class NoteService
    {
        private readonly INoteRepository repo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor http;

        public NoteService(INoteRepository repo, IMapper mapper, IHttpContextAccessor http)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.http = http;
        }

        private int GetUserId() => http.HttpContext.GetUserId();

        public async Task<List<NoteDto>> GetAllAsync()
        {
            var userId = GetUserId();
            var notes = await repo.GetAllAsync(userId);
            return mapper.Map<List<NoteDto>>(notes);
        }

        public async Task<NoteDto?> GetByIdAsync(Guid id)
        {
            var userId = GetUserId();
            var note = await repo.GetByIdAsync(id, userId);
            return note == null ? null : mapper.Map<NoteDto>(note);
        }

        public async Task<NoteDto> CreateAsync(CreateNoteDto dto)
        {
            var userId = GetUserId();
            var note = mapper.Map<Note>(dto);
            note.UserId = userId;

            await repo.AddAsync(note);
            return mapper.Map<NoteDto>(note);
        }

        public async Task<bool> UpdateAsync(Guid id, CreateNoteDto dto)
        {
            var userId = GetUserId();
            var note = await repo.GetByIdAsync(id, userId);
            if (note == null) return false;

            mapper.Map(dto, note);
            note.UpdatedAt = DateTime.UtcNow;

            await repo.UpdateAsync(note);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var userId = GetUserId();
            var note = await repo.GetByIdAsync(id, userId);
            if (note == null) return false;

            await repo.SoftDeleteAsync(note);
            return true;
        }
        public async Task<List<NoteDto>> GetDeletedAsync()
        {
            var userId = GetUserId();
            var notes = await repo.GetDeletedAsync(userId);

            return mapper.Map<List<NoteDto>>(notes);
        }
        public async Task<NoteDto?> RestoreAsync(Guid id)
        {
            var userId = GetUserId();
            var note = await repo.RestoreAsync(id, userId);
            return note == null ? null : mapper.Map<NoteDto>(note);
        }
    }
}
