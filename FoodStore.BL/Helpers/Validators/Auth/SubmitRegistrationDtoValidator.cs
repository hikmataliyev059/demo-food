using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Auth;

namespace FoodStore.BL.Helpers.Validators.Auth;

public class SubmitRegistrationDtoValidator : AbstractValidator<SubmitRegistrationDto>
{
    public SubmitRegistrationDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmail);

        RuleFor(x => x.ConfirmKey)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.Required);
    }
}