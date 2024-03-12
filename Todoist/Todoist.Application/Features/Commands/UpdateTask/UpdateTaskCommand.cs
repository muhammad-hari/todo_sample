using MediatR;
using Todoist.Domain.Common;
using Todoist.Domain.DataTypes;

namespace Todoist.Application.Features.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string ActivitiesNo { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriorityTypes Priority { get; set; }
        public TaskStatusTypes Status { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
