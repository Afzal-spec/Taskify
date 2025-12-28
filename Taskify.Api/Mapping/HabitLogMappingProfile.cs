using AutoMapper;
using Taskify.Api.DTOs.HabitsDtos;
using Taskify.Api.Models;


namespace Taskify.Api.Mapping
{
    public class HabitLogMappingProfile : Profile 
    {
        public HabitLogMappingProfile()
        {
            CreateMap<HabitLog, HabitLogDto>();
            CreateMap<CreateHabitLogDto, HabitLog>();
            CreateMap<HabitLog, HabitLogDtoWithHabit>()
                .ForMember(dest => dest.HabitName,
                    opt => opt.MapFrom(src => src.Habit.Name))
                .ForMember(dest => dest.HabitDescription,
                    opt => opt.MapFrom(src => src.Habit.Description))
                .ForMember(dest => dest.Frequency,
                    opt => opt.MapFrom(src => src.Habit.Frequency));

            CreateMap<(Habit habit, int current, int longest), HabitStreakDto>()
                .ForMember(d => d.HabitId, o => o.MapFrom(s => s.habit.Id))
                .ForMember(d => d.HabitName, o => o.MapFrom(s => s.habit.Name))
                .ForMember(d => d.Frequency, o => o.MapFrom(s => s.habit.Frequency))
                .ForMember(d => d.CurrentStreak, o => o.MapFrom(s => s.current))
                .ForMember(d => d.LongestStreak, o => o.MapFrom(s => s.longest));

        }
    }
}
