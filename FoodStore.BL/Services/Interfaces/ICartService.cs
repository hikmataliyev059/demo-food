using FoodStore.BL.Helpers.DTOs.Product;

namespace FoodStore.BL.Services.Interfaces;

public interface ICartService
{
    Task AddToCartAsync(int productId, string userId, int quantity);
    
    Task<IEnumerable<ProductGetDto>> GetCartAsync(string userId);
    
    Task UpdateCartAsync(int productId, string userId, int quantity);
    
    Task RemoveFromCartAsync(int productId, string userId);
    
    Task ClearCartAsync(string userId);
}