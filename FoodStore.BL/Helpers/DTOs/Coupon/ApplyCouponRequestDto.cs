namespace FoodStore.BL.Helpers.DTOs.Coupon;

public record ApplyCouponRequestDto
{
    public string CouponCode { get; set; }
    public decimal TotalAmount { get; set; }
}