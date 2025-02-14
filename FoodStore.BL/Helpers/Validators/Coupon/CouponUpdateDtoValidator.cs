using FluentValidation;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Coupon;

namespace FoodStore.BL.Helpers.Validators.Coupon;

public class CouponUpdateDtoValidator : AbstractValidator<CouponUpdateDto>
{
    public CouponUpdateDtoValidator()
    {
        RuleFor(c => c.DiscountAmount).GreaterThan(0).WithMessage(ValidationMessages.GreaterThanZero);

        RuleFor(c => c.ExpiryDate).GreaterThan(DateTime.UtcNow).WithMessage("Coupon expiry date must be in the future");
    }
}