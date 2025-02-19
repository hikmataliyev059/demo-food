using FoodStore.Core.Entities.Wish;
using FoodStore.Core.Repositories.Interfaces.Wishlists;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;

namespace FoodStore.DAL.Repositories.Implements.Wishlists;

public class WishlistRepository : GenericRepository<Wishlist>, IWishlistRepository
{
    public WishlistRepository(FoodStoreDbContext context) : base(context)
    {
    }
}