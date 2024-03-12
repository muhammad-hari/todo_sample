using AutoMapper;
using MassTransit.Transports;
using MediatR;
using Microsoft.Extensions.Logging;
using Todoist.Application.Contracts.Persistence;
using Todoist.Domain.Entities;
using Todoist.Domain.Events;

namespace Todoist.Application.Features.Queries.GetTaskList
{
    public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, List<TasksQuery>>
    {
        #region Private Members

        private readonly ITasksReplicateRepository tasksRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetTaskListQueryHandler> logger;

        #endregion

        #region Default Constructor

        public GetTaskListQueryHandler(ITasksReplicateRepository tasksRepository, IMapper mapper, ILogger<GetTaskListQueryHandler> logger)
        {
            this.tasksRepository = tasksRepository ?? throw new ArgumentNullException(nameof(tasksRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion


        public async Task<List<TasksQuery>> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var taskList = await tasksRepository.GetAllActiveTaskAsync();
                var result = mapper.Map<List<TasksQuery>>(taskList);
                return result;
            }
            catch (Exception ex)
            {
                // TODO:
                // Add specific error message for users
                logger.LogInformation($"An error occurred: ${ex.Message}");

                return new List<TasksQuery>();
            }

          
        }
    }
}
