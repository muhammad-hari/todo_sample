using FluentValidation;

namespace Todoist.Application.Features.Commands.CreateTask
{
    public class CreateTaskCommandValidation : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidation()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{Name} must not exceed 100 characters.");

            RuleFor(p => p.Priority)
               .NotEmpty().WithMessage("{Priority} is required.");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("{Status} is required.");

            RuleFor(p => p.ActivitiesNo)
                .MaximumLength(10).WithMessage("{ActivitiesNo} must not exceed 10 characters.")
                .NotEmpty().WithMessage("{Status} is required.");

        }
    }
}
