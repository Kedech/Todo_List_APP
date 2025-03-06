using FluentValidation;
using Todo_List_API.DTOs;

namespace Todo_List_API.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name)
                .NotNull().WithMessage("Name is required")
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(10).WithMessage("Name length would be more than 10 characters");

            RuleFor(user => user.Email)
                .NotNull().WithMessage("Email is required")
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MinimumLength(5).WithMessage("Email length would be more than 5 characters");

            RuleFor(user => user.Password)
                .NotNull().WithMessage("Password is required")
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password length would be more than 6 characters")
                .MaximumLength(50).WithMessage("Password length would be less than 50 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character")
                .Matches(@"^\S*$").WithMessage("Password must not contain spaces");
        }
    }
}
