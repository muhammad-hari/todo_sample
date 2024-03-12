using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoist.Domain.Entities;

namespace Todoist.Application.Contracts.Persistence
{
    public interface ITasksRepository : IAsyncRepository<Tasks>
    {
        Task<Tasks?> FindInActiveTaskByNoAsync(string taskNumber);
        Task<Tasks?> FindInActiveTaskByIDAsync(int taskId);
        Task<bool> SoftDeleteTaskByIDAsync(int taskId); 
    }
}
