using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoist.Domain.DataTypes;

namespace Todoist.Application.Features.Queries.GetTaskList
{
    public class TasksQuery
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
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
