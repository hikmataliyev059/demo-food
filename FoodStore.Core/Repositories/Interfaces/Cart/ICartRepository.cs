using FoodStore.Core.Entities.Cart;
using FoodStore.Core.Repositories.Interfaces.Common;

namespace FoodStore.Core.Repositories.Interfaces.Cart;

public interface ICartRepository : IGenericRepository<CartItem>;