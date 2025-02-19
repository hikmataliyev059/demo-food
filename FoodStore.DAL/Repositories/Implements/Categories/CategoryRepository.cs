using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Repositories.Interfaces.Categories;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;

namespace FoodStore.DAL.Repositories.Implements.Categories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(FoodStoreDbContext context) : base(context)
    {
    }
}