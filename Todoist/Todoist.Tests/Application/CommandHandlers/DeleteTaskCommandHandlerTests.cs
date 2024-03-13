using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Todoist.Application.Contracts.Persistence;
using Todoist.Application.Features.Commands.CreateTask;
using Todoist.Application.Features.Commands.DeleteTask;

namespace Todoist.Tests.Application.CommandHandlers
{
    [TestFixture]
    public class DeleteTaskCommandHandlerTests
    {
        private DeleteTaskCommandHandler _handler;
        private Mock<ITasksRepository> _mockTasksRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<CreateTaskCommandHandler>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockTasksRepository = new Mock<ITasksRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CreateTaskCommandHandler>>();

            _handler = new DeleteTaskCommandHandler(
                _mockTasksRepository.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var request = new DeleteTaskCommand { Id = 1 };
            _mockTasksRepository.Setup(r => r.SoftDeleteTaskByIDAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
      
        }

        [Test]
        public async Task Handle_DeleteFailed_ReturnsFailure()
        {
            // Arrange
            var request = new DeleteTaskCommand { Id = 1 };
            _mockTasksRepository.Setup(r => r.SoftDeleteTaskByIDAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
       
        }

        [Test]
        public async Task Handle_ExceptionThrown_ReturnsFailure()
        {
            // Arrange
            var request = new DeleteTaskCommand { Id = 1 };
            var exceptionMessage = "Test exception";
            _mockTasksRepository.Setup(r => r.SoftDeleteTaskByIDAsync(It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
