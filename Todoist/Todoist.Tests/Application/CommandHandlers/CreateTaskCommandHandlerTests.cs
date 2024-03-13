using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Todoist.Application.Contracts.Persistence;
using Todoist.Application.Features.Commands.CreateTask;
using Todoist.Domain.Entities;

namespace Todoist.Tests.Application.CommandHandlers
{
    [TestFixture]
    public class CreateTaskCommandHandlerTests
    {
        private CreateTaskCommandHandler _handler;
        private Mock<ITasksRepository> _mockTasksRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IPublishEndpoint> _mockPublishEndpoint;
        private Mock<ILogger<CreateTaskCommandHandler>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockTasksRepository = new Mock<ITasksRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockPublishEndpoint = new Mock<IPublishEndpoint>();
            _mockLogger = new Mock<ILogger<CreateTaskCommandHandler>>();

            _handler = new CreateTaskCommandHandler(
                _mockTasksRepository.Object,
                _mockMapper.Object,
                _mockPublishEndpoint.Object,
                _mockLogger.Object
            );
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var request = new CreateTaskCommand();
            var taskEntity = new Tasks { ActivitiesNo = "123456789" };
            var expectedResult = Tuple.Create("123456789", true);

            _mockMapper.Setup(m => m.Map<Tasks>(request)).Returns(taskEntity);
            _mockTasksRepository.Setup(r => r.AddAsync(taskEntity)).ReturnsAsync(taskEntity);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task Handle_ExceptionThrown_ReturnsFailure()
        {
            // Arrange
            var request = new CreateTaskCommand();
            var exceptionMessage = "Test exception";
            var expectedErrorMessage = "An error occurred: " + exceptionMessage;

            _mockMapper.Setup(m => m.Map<Tasks>(request)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

        }
    }
}
