using Todoist.Domain.Common;

namespace Todoist.Domain.Entities
{
    public class TaskTag : EntityBase
    {
        public int TaskId { get; set; }
        public int TagId { get; set; }
    }
}
