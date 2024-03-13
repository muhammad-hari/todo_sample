using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Todoist.Application.Contracts.Persistence;
using Todoist.Application.Features.Queries.GetTaskList;
using Todoist.Domain.Entities;

namespace Todoist.Tests.Application.QueryHandlers
{
    [TestFixture]
    public class GetTaskListQueryHandlerTests
    {
        private GetTaskListQueryHandler _handler;
        private Mock<ITasksReplicateRepository> _mockTasksRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<GetTaskListQueryHandler>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockTasksRepository = new Mock<ITasksReplicateRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetTaskListQueryHandler>>();

            _handler = new GetTaskListQueryHandler(
                _mockTasksRepository.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsTaskList()
        {
            // Arrange
            var request = new GetTaskListQuery();
            var taskList = new List<Tasks>(); // Assuming you have some task list here
            var mappedTaskList = new List<TasksQuery>(); // Assuming you have some mapped task list here

            _mockTasksRepository.Setup(r => r.GetAllActiveTaskAsync()).ReturnsAsync(taskList);
            _mockMapper.Setup(m => m.Map<List<TasksQuery>>(taskList)).Returns(mappedTaskList);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.AreEqual(mappedTaskList, result);
        }

        [Test]
        public async Task Handle_ExceptionThrown_ReturnsEmptyList()
        {
            // Arrange
            var request = new GetTaskListQuery();
            _mockTasksRepository.Setup(r => r.GetAllActiveTaskAsync()).Throws(new Exception("Test exception"));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
