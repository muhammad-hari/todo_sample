using Todoist.Domain.Entities;

namespace Todoist.Application.Contracts.Persistence
{
    public interface ITasksReplicateRepository : IAsyncRepository<Tasks>
    {
        Task<Tasks?> FindInActiveTaskByNoAsync(string taskNumber);
        Task<Tasks?> FindInActiveTaskByIDAsync(int taskId);
        Task<bool> SoftDeleteTaskByIDAsync(int taskId);
        Task<List<Tasks>> GetAllActiveTaskAsync();
    }
}
