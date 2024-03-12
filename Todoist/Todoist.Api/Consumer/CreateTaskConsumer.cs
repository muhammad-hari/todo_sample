using AutoMapper;
using MassTransit;
using MediatR;
using Todoist.Application.Contracts.Persistence;
using Todoist.Application.Features.Commands.CreateTask;
using Todoist.Domain.Entities;
using Todoist.Domain.Events;

namespace Todoist.Consumer
{
    public class CreateTaskConsumer : IConsumer<CreateTaskEvent>
    {
        private readonly IMapper mapper;
        private readonly ITasksReplicateRepository tasksReplicateRepository;
        private readonly ILogger<CreateTaskConsumer> logger;

        public CreateTaskConsumer(IMapper mapper, ITasksReplicateRepository tasksReplicateRepository, ILogger<CreateTaskConsumer> logger)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.tasksReplicateRepository = tasksReplicateRepository ?? throw new ArgumentNullException(nameof(tasksReplicateRepository));
        }

        public async Task Consume(ConsumeContext<CreateTaskEvent> context)
        {
            var tasks = mapper.Map<Tasks>(context.Message);
            var result = tasksReplicateRepository.AddAsync(tasks);

            logger.LogInformation($"CreateTaskEvent consumed successfully");
        }
    }
}
