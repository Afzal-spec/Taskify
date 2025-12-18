using AutoMapper;
using Taskify.Api.DTOs.NotesDtos;
using Taskify.Api.Models;

namespace Taskify.Api.Mapping
{
    public class NoteMappingProfile: Profile
    {
        public NoteMappingProfile()
        {
            CreateMap<CreateNoteDto, Note>();
            CreateMap<Note, NoteDto>();
        }
    }
}
