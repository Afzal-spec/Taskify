using AutoMapper;
using Taskify.Api.DTOs.HabitsDtos;
using Taskify.Api.Models;

namespace Taskify.Api.Mapping
{
    public class HabitMappingProfile : Profile
    {
        public HabitMappingProfile()
        {
            CreateMap<Habit, HabitDto>();
            CreateMap<CreateHabitDto, Habit>();
        }
    }
}
