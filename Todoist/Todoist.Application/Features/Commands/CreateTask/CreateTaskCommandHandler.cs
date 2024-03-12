using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Todoist.Application.Contracts.Persistence;
using Todoist.Domain.Entities;
using Todoist.Domain.Events;

namespace Todoist.Application.Features.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Tuple<string, bool>>
    {
        #region Private Members

        private readonly ITasksRepository tasksRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateTaskCommandHandler> logger;
        private readonly IPublishEndpoint publishEndpoint;

        #endregion

        #region Default Constructor

        public CreateTaskCommandHandler(ITasksRepository tasksRepository, IMapper mapper, IPublishEndpoint publishEndpoint, ILogger<CreateTaskCommandHandler> logger)
        {
            this.tasksRepository = tasksRepository ?? throw new ArgumentNullException(nameof(tasksRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint)); 
        }

        #endregion

        public async Task<Tuple<string, bool>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // this is only sample payload, generate unique number here
                request.ActivitiesNo = $"{DateTime.Now.ToString("yyyyMMddhhmmss")}";
                var taskEntity = mapper.Map<Tasks>(request);
                var newTask = await tasksRepository.AddAsync(taskEntity);

                // TODO:
                // Add some handler to prevent failed transaction before making a request
                var eventMessage = mapper.Map<CreateTaskEvent>(request);
                await publishEndpoint.Publish(eventMessage);

                logger.LogInformation($"Task is successfully created with task activity number: {newTask.ActivitiesNo}");

                // TODO:
                // Send email for notification here 
                // .....

                return Tuple.Create(newTask.ActivitiesNo, true);
            }
            catch (Exception ex)
            {
                // TODO:
                // Add specific error message for users
                logger.LogInformation($"An error occurred: ${ex.Message}");

                return Tuple.Create(string.Empty, false);
            }


        }
    }
}
