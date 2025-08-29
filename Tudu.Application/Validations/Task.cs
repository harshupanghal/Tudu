using FluentValidation;
using Tudu.Application.DTOs;

namespace Tudu.Application.Validations;

public class CreateUserTaskRequestValidator : AbstractValidator<CreateUserTaskRequest>
    {
    public CreateUserTaskRequestValidator()
        {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Task title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.DueDate)
          .GreaterThanOrEqualTo(DateTime.Today)
          .When(x => x.DueDate.HasValue)
          .WithMessage("Due date cannot be in the past.");

        RuleFor(x => x.Category)
            .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");
        }

    public class UpdateUserTaskRequestValidator : AbstractValidator<UpdateUserTaskRequest>
        {
        public UpdateUserTaskRequestValidator()
            {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid task ID.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .When(x => x.DueDate.HasValue)
                .WithMessage("Due date cannot be in the past.");

            RuleFor(x => x.Category)
                .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");

            //RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Invalid user ID.");
            }
        }
    }
