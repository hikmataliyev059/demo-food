using FoodStore.BL.Helpers.DTOs.Carts;

namespace FoodStore.BL.Services.Interfaces.Cart;

public interface ICartService
{
    Task AddToCartAsync(int productId, string userId, int quantity);
    
    Task<IEnumerable<CartGetDto>> GetCartAsync(string userId);
    
    Task UpdateCartAsync(int productId, string userId, int quantity);
    
    Task RemoveFromCartAsync(int productId, string userId);
    
    Task ClearCartAsync(string userId);
}