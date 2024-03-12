using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Todoist.Application.Features.Commands.CreateTask;
using Todoist.Application.Features.Commands.DeleteTask;
using Todoist.Application.Features.Commands.UpdateTask;
using Todoist.Application.Features.Queries.GetTaskList;

namespace Todoist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator mediator;

        public TasksController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<List<TasksQuery>>> GetAllTasks()
        {
            var query = new GetTaskListQuery();
            var tasks = await mediator.Send(query);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<Tuple<string, bool>>> CreateTask([FromBody] CreateTaskCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DeleteTask(int id)
        {
            var command = new DeleteTaskCommand() { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
