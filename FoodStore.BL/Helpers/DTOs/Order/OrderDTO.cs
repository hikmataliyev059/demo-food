using FoodStore.Core.Enums;

namespace FoodStore.BL.Helpers.DTOs.Order;

public record OrderDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
}