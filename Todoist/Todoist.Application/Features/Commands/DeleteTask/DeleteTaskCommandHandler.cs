using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Todoist.Application.Contracts.Persistence;
using Todoist.Application.Features.Commands.CreateTask;
using Todoist.Domain.Entities;

namespace Todoist.Application.Features.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        #region Private Members

        private readonly ITasksRepository tasksRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateTaskCommandHandler> logger;

        #endregion

        #region Default Constructor

        public DeleteTaskCommandHandler(ITasksRepository tasksRepository, IMapper mapper, ILogger<CreateTaskCommandHandler> logger)
        {
            this.tasksRepository = tasksRepository ?? throw new ArgumentNullException(nameof(tasksRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteTask = await tasksRepository.SoftDeleteTaskByIDAsync(request.Id);
                if(deleteTask)
                {
                    logger.LogInformation($"Task is successfully deleted with task id: {request.Id}");

                    // TODO:
                    // Send email for notification here 
                    // .....

                    return true;
                }


                return false;

            }
            catch (Exception ex)
            {
                // TODO:
                // Add specific error message for users
                logger.LogInformation($"An error occurred: ${ex.Message}");

                return false;
            }
        }
    }
}
