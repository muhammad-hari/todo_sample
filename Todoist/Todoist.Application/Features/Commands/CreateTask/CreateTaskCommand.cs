using MediatR;
using Todoist.Domain.Common;
using Todoist.Domain.DataTypes;

namespace Todoist.Application.Features.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<Tuple<string, bool>>
    {
        public string ActivitiesNo { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriorityTypes Priority { get; set; }
        public TaskStatusTypes Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
