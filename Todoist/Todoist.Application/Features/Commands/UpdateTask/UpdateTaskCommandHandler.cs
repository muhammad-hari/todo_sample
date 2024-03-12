using AutoMapper;
using MassTransit.Transports;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoist.Application.Contracts.Persistence;
using Todoist.Application.Features.Commands.CreateTask;
using Todoist.Domain.Entities;
using Todoist.Domain.Events;

namespace Todoist.Application.Features.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        #region Private Members

        private readonly ITasksRepository tasksRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateTaskCommandHandler> logger;

        #endregion

        #region Default Constructor

        public UpdateTaskCommandHandler(ITasksRepository tasksRepository, IMapper mapper, ILogger<UpdateTaskCommandHandler> logger)
        {
            this.tasksRepository = tasksRepository ?? throw new ArgumentNullException(nameof(tasksRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var getCurrentTask = await tasksRepository.FindInActiveTaskByIDAsync(request.Id);
                if (getCurrentTask == null)
                    // TODO: 
                    // Add specific response message for users
                    return false;
                

                mapper.Map(request, getCurrentTask, typeof(UpdateTaskCommand), typeof(Tasks));
                await tasksRepository.UpdateAsync(getCurrentTask);

                // TODO:
                // Add some handler to prevent failed transaction before making a request
                //var eventMessage = mapper.Map<UpdateTaskCommand>(request);
                //await publishEndpoint.Publish(eventMessage);

                logger.LogInformation($"Task is successfully updated with task activity number: {getCurrentTask.ActivitiesNo}");

                // TODO:
                // Send email for notification here 
                // .....

                return true;
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
