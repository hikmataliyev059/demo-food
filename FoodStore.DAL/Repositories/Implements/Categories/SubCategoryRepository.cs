using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Repositories.Interfaces.Categories;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;

namespace FoodStore.DAL.Repositories.Implements.Categories;

public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
{
    public SubCategoryRepository(FoodStoreDbContext context) : base(context)
    {
    }
}