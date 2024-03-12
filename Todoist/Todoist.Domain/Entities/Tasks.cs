using System.ComponentModel.DataAnnotations;
using Todoist.Domain.Common;
using Todoist.Domain.DataTypes;

namespace Todoist.Domain.Entities
{
    public class Tasks : EntityBase
    {
        public int Id { get; set; }
        public string ActivitiesNo { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriorityTypes Priority { get; set; }
        public TaskStatusTypes Status { get; set; }
    }
}
