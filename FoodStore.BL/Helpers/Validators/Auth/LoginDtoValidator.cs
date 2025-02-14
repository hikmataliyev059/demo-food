using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Auth;

namespace FoodStore.BL.Helpers.Validators.Auth;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.EmailOrUsernameRequired)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.MinLength)
            .MaximumLength(30)
            .WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.PasswordRequired)
            .MinimumLength(4)
            .WithMessage(ValidationMessages.MinLength)
            .Matches("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
            .WithMessage(ValidationMessages.PasswordCriteria);
    }
}