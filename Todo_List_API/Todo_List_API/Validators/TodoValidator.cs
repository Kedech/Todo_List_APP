using FluentValidation;
using Todo_List_API.DTOs;

namespace Todo_List_API.Validators
{
    public class TodoValidator : AbstractValidator<CreateUpdateTodoDTO>
    {
        public TodoValidator() {
            RuleFor(todo => todo.Title)
                .NotNull().WithMessage("Title is required")
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("Title length would be more than 3 characters");

            RuleFor(todo => todo.Description)
                .NotNull().WithMessage("Description is required")
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(3).WithMessage("Description length would be more than 3 characters");
        }
    }
}
