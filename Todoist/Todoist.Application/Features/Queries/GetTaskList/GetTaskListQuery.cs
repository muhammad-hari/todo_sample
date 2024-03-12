using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todoist.Application.Features.Queries.GetTaskList
{
    public class GetTaskListQuery : IRequest<List<TasksQuery>>
    {

    }
}
