using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Auth;

namespace FoodStore.BL.Helpers.Validators.Auth;

public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MinimumLength(3).WithMessage(ValidationMessages.MinLength)
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmail);

        RuleFor(x => x.Token)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.PasswordRequired)
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