using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Payment;

namespace FoodStore.BL.Helpers.Validators.Payment
{
    public class PaymentRequestDTOValidator : AbstractValidator<PaymentRequestDTO>
    {
        public PaymentRequestDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessages.Required)
                .EmailAddress().WithMessage(ValidationMessages.InvalidEmail);

            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessages.Required)
                .Matches(@"^\d{16}$").WithMessage("Kart nömrəsi 16 rəqəmdən ibarət olmalıdır.");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessages.Required)
                .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$").WithMessage("Expiration Date düzgün formatda olmalıdır: MM/YY")
                .Must(BeAValidFutureDate).WithMessage("Expiration Date gələcək bir tarix olmalıdır.");

            RuleFor(x => x.Cvc)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessages.Required)
                .Matches(@"^\d{3}$").WithMessage("CVC 3 rəqəmdən ibarət olmalıdır.");

            RuleFor(x => x.NameOnCard)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessages.Required)
                .MaximumLength(100).WithMessage(ValidationMessages.MaxLength);

            RuleFor(x => x.CountryOrRegion)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessages.Required);

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage(ValidationMessages.Required)
                .Matches(@"^\d{4,10}$")
                .WithMessage("Poçt kodu yalnız rəqəmlərdən ibarət olmalı və 4-10 rəqəm arasında olmalıdır");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }

        private static bool BeAValidFutureDate(string expirationDate)
        {
            var monthYear = expirationDate.Split('/');

            if (monthYear.Length != 2)
                return false;

            var month = int.Parse(monthYear[0]);
            var year = int.Parse(monthYear[1]);

            var currentYear = DateTime.Now.Year % 100;
            var currentMonth = DateTime.Now.Month;

            if (year < currentYear || (year == currentYear && month < currentMonth))
            {
                return false;
            }

            return true;
        }
    }
}