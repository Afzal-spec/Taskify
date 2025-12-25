using AutoMapper;
using Taskify.Api.DTOs.JournalDtos;
using Taskify.Api.Models;

namespace Taskify.Api.Mapping
{
    public class JournalMappingProfile : Profile
    {
        public JournalMappingProfile()
        {
            CreateMap<CreateJournalEntryDto, JournalEntry>();
            CreateMap<JournalEntry, JournalEntryDto>();
        }
    }
}
