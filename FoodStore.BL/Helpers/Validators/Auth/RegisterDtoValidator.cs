using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Auth;

namespace FoodStore.BL.Helpers.Validators.Auth;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(3).WithMessage(ValidationMessages.MinLength)
            .MaximumLength(30).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(3).WithMessage(ValidationMessages.MinLength)
            .MaximumLength(30).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(3).WithMessage(ValidationMessages.MinLength)
            .MaximumLength(30).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(3).WithMessage(ValidationMessages.MinLength)
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmail);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(4).WithMessage(ValidationMessages.MinLength)
            .Matches("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
            .WithMessage(ValidationMessages.PasswordCriteria);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(4).WithMessage(ValidationMessages.MinLength)
            .Equal(x => x.Password).WithMessage(ValidationMessages.PasswordMismatch);
    }
}