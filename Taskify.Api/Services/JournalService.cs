using AutoMapper;
using Taskify.Api.DTOs.JournalDtos;
using Taskify.Api.Helpers;
using Taskify.Api.Models;
using Taskify.Api.Repositories;

namespace Taskify.Api.Services
{
    public class JournalService
    {
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor http;
        private readonly IJournalRepository repo;

        public JournalService(IMapper mapper, IHttpContextAccessor http, IJournalRepository repo)
        {
            this.mapper = mapper;
            this.http = http;
            this.repo = repo;
        }

        private int GetUserID()
        {
            return http.HttpContext!.GetUserId();
        }
        public async Task<JournalEntryDto> UpsertAsync(CreateJournalEntryDto dto)
        {
            var userId = GetUserID();
            var existing = await repo.GetByDateAsync(dto.Date, userId);
            if(existing == null)
            {
                var entry = mapper.Map<JournalEntry>(dto);
                entry.UserId = userId;
                await repo.AddAsync(entry);
                return mapper.Map<JournalEntryDto>(entry);
            }
            existing.Content = dto.Content;
            existing.Mood = dto.Mood;
            await repo.UpdateAsync(existing);
            return mapper.Map<JournalEntryDto>(existing);
        }
        public async Task<List<JournalEntryDto>> GetAllAsync()
        {
            var userId = GetUserID();
            var entries = await repo.GetAllAsync(userId);
            return mapper.Map<List<JournalEntryDto>>(entries);
        }
        public async Task<bool> DeleteAsync(DateTime date)
        {
            var userId = GetUserID();
            var entry = await repo.GetByDateAsync(date, userId);
            if (entry == null) return false;
            await repo.SoftDeleteAsync(entry);
            return true;
        }
        public async Task<List<JournalEntryDto>> GetDeletedAsync()
        {
            var userId = GetUserID();
            var entries = await repo.GetDeletedAsync(userId);
            return mapper.Map<List<JournalEntryDto>>(entries);
        }

        public async Task<bool> RestoreAsync(DateTime date)
        {
            var userId = GetUserID();
            var entry = await repo.GetDeletedByDateAsync(date, userId);
            if (entry == null) return false;
            entry.IsDeleted = false;
            await repo.UpdateAsync(entry);
            return true;
        }

    }
}
