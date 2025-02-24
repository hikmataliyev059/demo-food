using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Payment;

namespace FoodStore.BL.Helpers.Validators.Payment;

public class PaymentDtoValidator : AbstractValidator<PaymentDto>
{
    public PaymentDtoValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage(ValidationMessages.Required)
            .Matches(@"^tok_\w+$").WithMessage("Invalid token format");

        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);

        RuleFor(x => x.BillingName).NotEmpty().WithMessage(ValidationMessages.Required)
            .MaximumLength(100).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.BillingEmail).NotEmpty().WithMessage(ValidationMessages.Required)
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmail);

        RuleFor(x => x.BillingPhone).NotEmpty().WithMessage(ValidationMessages.Required)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage(ValidationMessages.InvalidPhoneNumber);

        RuleFor(x => x.BillingAddress).NotNull().WithMessage(ValidationMessages.Required)
            .SetValidator(new BillingAddressDtoValidator());
    }
}