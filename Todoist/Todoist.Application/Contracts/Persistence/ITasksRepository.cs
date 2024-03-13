using Todoist.Domain.Entities;

namespace Todoist.Application.Contracts.Persistence
{
    public interface ITasksReplicateRepository : IAsyncRepository<Tasks>
    {
        Task<Tasks?> FindInActiveTaskByNoAsync(string taskNumber);
        Task<Tasks?> FindInActiveTaskByIDAsync(int taskId);
        Task<bool> SoftDeleteTaskByIDAsync(int taskId);

        // TODO:
        // We can add pagination and filters to limit the data retrieved based on requests in order to improve application performance

        Task<List<Tasks>> GetAllActiveTaskAsync();
    }
}
