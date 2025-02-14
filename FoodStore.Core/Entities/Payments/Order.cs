using FoodStore.Core.Entities.Common;
using FoodStore.Core.Enums;

namespace FoodStore.Core.Entities.Payments;

public class Order : BaseEntity
{
    public decimal Amount { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public string PaymentIntentId { get; set; } 
    public string ReceiptEmail { get; set; }
}