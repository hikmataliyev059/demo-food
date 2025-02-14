using FoodStore.Core.Entities.Wishlists;

namespace FoodStore.Core.Repositories.Interfaces;

public interface IWishlistRepository : IGenericRepository<Wishlist>
{
    // Task<ProductWishlist?> GetWishlistByProductAsync(int productId); 
    // Task<Wishlist?> GetActiveWishlistAsync();
}