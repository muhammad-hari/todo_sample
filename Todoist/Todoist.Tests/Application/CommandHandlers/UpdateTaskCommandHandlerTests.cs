using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Todoist.Application.Contracts.Persistence;
using Todoist.Application.Features.Commands.UpdateTask;
using Todoist.Domain.Entities;

namespace Todoist.Tests.Application.CommandHandlers
{
    [TestFixture]
    public class UpdateTaskCommandHandlerTests
    {
        private UpdateTaskCommandHandler _handler;
        private Mock<ITasksRepository> _mockTasksRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<UpdateTaskCommandHandler>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockTasksRepository = new Mock<ITasksRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<UpdateTaskCommandHandler>>();

            _handler = new UpdateTaskCommandHandler(
                _mockTasksRepository.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var request = new UpdateTaskCommand { Id = 1 /* sample ID */, /* other properties */ };
            var existingTask = new Tasks(); // Create a mock existing task
            _mockTasksRepository.Setup(r => r.FindInActiveTaskByIDAsync(It.IsAny<int>())).ReturnsAsync(existingTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            _mockTasksRepository.Verify(
                x => x.UpdateAsync(existingTask),
                Times.Once);
        }

        [Test]
        public async Task Handle_TaskNotFound_ReturnsFailure()
        {
            // Arrange
            var request = new UpdateTaskCommand { Id = 1 /* sample ID */, /* other properties */ };
            _mockTasksRepository.Setup(r => r.FindInActiveTaskByIDAsync(It.IsAny<int>())).ReturnsAsync((Tasks)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
            _mockTasksRepository.Verify(
                x => x.UpdateAsync(It.IsAny<Tasks>()),
                Times.Never);
        }

        [Test]
        public async Task Handle_ExceptionThrown_ReturnsFailure()
        {
            // Arrange
            var request = new UpdateTaskCommand { Id = 1 /* sample ID */, /* other properties */ };
            var exceptionMessage = "Test exception";
            _mockTasksRepository.Setup(r => r.FindInActiveTaskByIDAsync(It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
