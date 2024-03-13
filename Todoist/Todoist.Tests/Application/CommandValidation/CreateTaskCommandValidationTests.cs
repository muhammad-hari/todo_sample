using FluentValidation.TestHelper;
using NUnit.Framework;
using Todoist.Application.Features.Commands.CreateTask;

namespace Todoist.Tests.Application.CommandValidation
{
    [TestFixture]
    public class CreateTaskCommandValidationTests
    {
        private CreateTaskCommandValidation _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateTaskCommandValidation();
        }

        [Test]
        public void Validate_WhenNameIsNull_ShouldHaveError()
        {
            var command = new CreateTaskCommand { Name = null };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("{Name} is required.");
        }

        [Test]
        public void Validate_WhenNameExceedsMaxLength_ShouldHaveError()
        {
            var command = new CreateTaskCommand { Name = new string('a', 101) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("{Name} must not exceed 100 characters.");
        }

        [Test]
        public void Validate_WhenActivitiesNoExceedsMaxLength_ShouldHaveError()
        {
            var command = new CreateTaskCommand { ActivitiesNo = new string('a', 11) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ActivitiesNo)
                .WithErrorMessage("{ActivitiesNo} must not exceed 10 characters.");
        }

        [Test]
        public void Validate_WhenActivitiesNoIsNull_ShouldHaveError()
        {
            var command = new CreateTaskCommand { ActivitiesNo = null };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ActivitiesNo)
                .WithErrorMessage("{ActivitiesNo} is required.");
        }
    }
}
