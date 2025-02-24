using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Contacts;

namespace FoodStore.BL.Helpers.Validators.Contacts;

public class ContactUpdateDtoValidator : AbstractValidator<ContactUpdateDto>
{
    public ContactUpdateDtoValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .MaximumLength(100).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmail)
            .MaximumLength(150).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .Matches(@"^\+[1-9]\d{1,14}$").WithMessage(ValidationMessages.InvalidPhoneNumber)
            .MaximumLength(15).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.Subject).NotEmpty().NotNull().WithMessage(ValidationMessages.Required)
            .MaximumLength(200).WithMessage(ValidationMessages.MaxLength);

        RuleFor(x => x.Message).NotEmpty().NotNull().WithMessage(ValidationMessages.Required);
    }
}