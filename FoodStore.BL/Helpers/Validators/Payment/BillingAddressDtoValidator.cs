using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Payment;

namespace FoodStore.BL.Helpers.Validators.Payment;

public class BillingAddressDtoValidator : AbstractValidator<BillingAddressDto>
{
    public BillingAddressDtoValidator()
    {
        RuleFor(x => x.Line1).NotEmpty().WithMessage(ValidationMessages.Required)
            .MaximumLength(150).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.Line2).MaximumLength(150).WithMessage(ValidationMessages.MaxLength)
            .When(x => !string.IsNullOrEmpty(x.Line2));

        RuleFor(x => x.City).NotEmpty().WithMessage(ValidationMessages.Required)
            .MaximumLength(100).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.State).NotEmpty().WithMessage(ValidationMessages.Required)
            .MaximumLength(100).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.PostalCode).NotEmpty().WithMessage(ValidationMessages.Required)
            .Matches(@"^[A-Za-z0-9]{4,10}$").WithMessage("Invalid postal code format");

        RuleFor(x => x.Country).NotEmpty().WithMessage(ValidationMessages.Required)
            .Length(2).WithMessage("Country code must be 2 characters (ISO 3166-1 alpha-2)");
    }
}