using FoodStore.Core.Entities.Cart;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;

namespace FoodStore.DAL.Repositories.Implements;

public class CartRepository : GenericRepository<CartItem>, ICartRepository
{
    public CartRepository(FoodStoreDbContext context) : base(context)
    {
    }
}