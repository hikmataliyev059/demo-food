using FoodStore.Core.Entities.Wishlists;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;

namespace FoodStore.DAL.Repositories.Implements;

public class WishlistRepository : GenericRepository<Wishlist>, IWishlistRepository
{
    public WishlistRepository(FoodStoreDbContext context) : base(context)
    {
    }

    // private readonly FoodStoreDbContext _context;
    //
    // public WishlistRepository(FoodStoreDbContext context) : base(context)
    // {
    //     _context = context;
    // }
    //
    // public async Task<ProductWishlist?> GetWishlistByProductAsync(int productId)
    // {
    //     return await _context.ProductWishlists.FirstOrDefaultAsync(pw => pw.ProductId == productId && !pw.IsDeleted);
    // }
    //
    // public async Task<Wishlist?> GetActiveWishlistAsync()
    // {
    //     return await _context.Wishlists.FirstOrDefaultAsync(w => w.IsActive && !w.IsDeleted);
    // }
}