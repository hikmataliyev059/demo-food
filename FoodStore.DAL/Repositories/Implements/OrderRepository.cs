using FoodStore.Core.Entities.Payments;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;

namespace FoodStore.DAL.Repositories.Implements;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(FoodStoreDbContext context) : base(context)
    {
    }
}