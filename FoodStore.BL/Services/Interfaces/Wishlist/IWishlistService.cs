using FoodStore.BL.Helpers.DTOs.Product;

namespace FoodStore.BL.Services.Interfaces.Wishlist;

public interface IWishlistService
{
    Task AddToWishlistAsync(int productId, string userId);

    Task RemoveFromWishlistAsync(int productId, string userId);

    Task<IEnumerable<ProductGetDto>> GetWishlistAsync(string userId);
}