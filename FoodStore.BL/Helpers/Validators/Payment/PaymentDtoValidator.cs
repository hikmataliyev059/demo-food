using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Payment;

namespace FoodStore.BL.Helpers.Validators.Payment;

public class PaymentDtoValidator : AbstractValidator<PaymentDto>
{
    public PaymentDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required")
            .Matches(@"^tok_\w+$").WithMessage("Invalid token format");

        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);
    }
}