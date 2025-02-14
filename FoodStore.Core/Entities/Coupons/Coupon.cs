using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Coupons;

public class Coupon : BaseEntity
{
    public string Code { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsPercentage { get; set; }
}