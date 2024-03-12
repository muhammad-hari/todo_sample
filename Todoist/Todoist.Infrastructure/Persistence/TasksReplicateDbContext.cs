using Microsoft.EntityFrameworkCore;
using Todoist.Domain.Entities;

namespace Todoist.Infrastructure.Persistence
{
    public class TasksReplicateDbContext : DbContext
    {
        public TasksReplicateDbContext(DbContextOptions<TasksReplicateDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
        }


        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
