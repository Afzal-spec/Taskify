using AutoMapper;
using Taskify.Api.DTOs;
using Taskify.Api.Models;

namespace Taskify.Api.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<CreateTaskItemDto, TaskItem>().ReverseMap();
            CreateMap<UpdateTaskItemDto, TaskItem>().ReverseMap();
            CreateMap<TaskItemDto, TaskItem>().ReverseMap();
        }
    }
}
