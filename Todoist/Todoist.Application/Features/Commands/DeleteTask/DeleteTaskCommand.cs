using MediatR;

namespace Todoist.Application.Features.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
