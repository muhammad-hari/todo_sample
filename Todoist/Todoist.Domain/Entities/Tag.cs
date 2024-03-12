using System.ComponentModel.DataAnnotations;
using Todoist.Domain.Common;

namespace Todoist.Domain.Entities
{
    public class Tag : EntityBase
    {
        public int Id { get; set; } 
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
