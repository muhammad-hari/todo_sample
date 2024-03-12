using Todoist.Domain.DataTypes;

namespace Todoist.Domain.Events
{
    public class CreateTaskEvent
    {
        public int Id { get; set; }
        public string ActivitiesNo { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriorityTypes Priority { get; set; }
        public TaskStatusTypes Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
