using FoodStore.BL.Helpers.DTOs.Coupon;

namespace FoodStore.BL.Services.Interfaces.Coupon;

public interface ICouponService
{
    Task<decimal> ApplyCouponAsync(ApplyCouponRequestDto couponRequestDto);

    Task AddCouponAsync(CouponDto couponDto);

    Task DeleteCouponAsync(string couponCode);

    Task UpdateCouponAsync(string couponCode, CouponUpdateDto couponDto);

    Task<CouponDto?> GetCouponAsync(string couponCode);

    Task<IEnumerable<CouponDto>> GetAllCouponsAsync();
}