namespace FoodStore.BL.Helpers.DTOs.Cart;

public record CartDto
{
    public int UserId { get; set; }
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    // public decimal SubTotal => Items.Sum(item => item.SubTotal);
}