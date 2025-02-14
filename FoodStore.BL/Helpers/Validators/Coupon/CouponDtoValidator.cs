using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Coupon;

namespace FoodStore.BL.Helpers.Validators.Coupon;

public class CouponDtoValidator : AbstractValidator<CouponDto>
{
    public CouponDtoValidator()
    {
        RuleFor(c => c.Code).NotEmpty().WithMessage(ValidationMessages.Required)
            .Length(5, 50).WithMessage("Coupon code must be between 5 and 50 characters");

        RuleFor(c => c.DiscountAmount).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);

        RuleFor(c => c.ExpiryDate).GreaterThan(DateTime.UtcNow).WithMessage("Coupon expiry date must be in the future");
    }
}