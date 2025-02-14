namespace FoodStore.BL.Helpers.DTOs.Cart;

public record CartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; } 
    // public decimal SubTotal => Price * Quantity;
}