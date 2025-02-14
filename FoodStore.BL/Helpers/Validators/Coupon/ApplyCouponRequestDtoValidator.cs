using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Coupon;

namespace FoodStore.BL.Helpers.Validators.Coupon;

public class ApplyCouponRequestDtoValidator : AbstractValidator<ApplyCouponRequestDto>
{
    public ApplyCouponRequestDtoValidator()
    {
        RuleFor(c => c.CouponCode)
            .NotEmpty().WithMessage(ValidationMessages.Required)
            .Length(5, 50).WithMessage("Coupon code must be between 5 and 50 characters");

        RuleFor(c => c.TotalAmount)
            .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);
    }
}