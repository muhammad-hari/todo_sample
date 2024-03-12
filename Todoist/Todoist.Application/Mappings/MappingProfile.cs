using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoist.Application.Features.Commands.CreateTask;
using Todoist.Application.Features.Commands.UpdateTask;
using Todoist.Application.Features.Queries.GetTaskList;
using Todoist.Domain.Entities;

namespace Todoist.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tasks, CreateTaskCommand>().ReverseMap();
            CreateMap<Tasks, UpdateTaskCommand>().ReverseMap();
            CreateMap<Tasks, TasksQuery>().ReverseMap();
        }
    }
}
