using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Todoist.Application.Contracts.Persistence;
using Todoist.Domain.Entities;
using Todoist.Infrastructure.Persistence;

namespace Todoist.Infrastructure.Repositories
{
    public class TasksReplicateRepository : BaseReplicateRepository<Tasks>, ITasksReplicateRepository
    {
        public TasksReplicateRepository(TasksReplicateDbContext dbContext) : base(dbContext) { }

        public async Task<Tasks?> FindInActiveTaskByIDAsync(int taskId) 
            => await dbContext.Tasks.Where(x => x.IsDeleted == false && x.Id == taskId).FirstOrDefaultAsync();

        public async Task<List<Tasks>> GetAllActiveTaskAsync()
            => await dbContext.Tasks.Where(x => x.IsDeleted == false).AsNoTracking().ToListAsync();

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
