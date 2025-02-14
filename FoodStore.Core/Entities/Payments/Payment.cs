using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Payments;

public class Payment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}