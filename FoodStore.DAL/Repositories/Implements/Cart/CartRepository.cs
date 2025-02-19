using FoodStore.Core.Entities.Cart;
using FoodStore.Core.Repositories.Interfaces.Cart;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;

namespace FoodStore.DAL.Repositories.Implements.Cart;

public class CartRepository : GenericRepository<CartItem>, ICartRepository
{
    public CartRepository(FoodStoreDbContext context) : base(context)
    {
    }
}