using AutoMapper;
using Todoist.Application.Features.Commands.CreateTask;
using Todoist.Domain.Entities;
using Todoist.Domain.Events;

namespace Todoist.Mapper
{
    public class CreateTaskProfile : Profile
    {
        public CreateTaskProfile()
        {
            CreateMap<CreateTaskCommand, CreateTaskEvent>().ReverseMap();
            CreateMap<Tasks, CreateTaskEvent>().ReverseMap();
        }
    }
}
