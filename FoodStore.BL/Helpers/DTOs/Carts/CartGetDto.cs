using FoodStore.BL.Helpers.DTOs.Product;

namespace FoodStore.BL.Helpers.DTOs.Carts;

public record CartGetDto
{
    public ProductGetDto Product { get; set; }
    public int Quantity { get; set; }
}