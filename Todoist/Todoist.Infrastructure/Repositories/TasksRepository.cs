using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Todoist.Application.Contracts.Persistence;
using Todoist.Domain.Entities;
using Todoist.Infrastructure.Persistence;

namespace Todoist.Infrastructure.Repositories
{
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {
        public TasksRepository(TaskDbContext dbContext) : base(dbContext) { }

        public async Task<Tasks?> FindInActiveTaskByIDAsync(int taskId) 
            => await dbContext.Tasks.Where(x => x.IsDeleted == false && x.Id == taskId).FirstOrDefaultAsync();

        public async Task<Tasks?> FindInActiveTaskByNoAsync(string taskNumber)
            => await dbContext.Tasks.Where(x => x.IsDeleted == false && x.ActivitiesNo == taskNumber).FirstOrDefaultAsync();

        public async Task<bool> SoftDeleteTaskByIDAsync(int taskId)
        {
            var currentTask = await FindInActiveTaskByIDAsync(taskId);
            if (currentTask != null)
            {
                currentTask.IsDeleted = true;
                dbContext.SaveChanges();
                return true;
            }

            return false;   
        }
    }
}
