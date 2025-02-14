namespace FoodStore.BL.Helpers.DTOs.Coupon;

public record CouponDto
{
    public string Code { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsPercentage { get; set; }
}