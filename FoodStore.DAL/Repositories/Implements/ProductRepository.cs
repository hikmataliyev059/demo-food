using FoodStore.Core.Entities.Products;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;

namespace FoodStore.DAL.Repositories.Implements;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(FoodStoreDbContext context) : base(context)
    {
    }
}