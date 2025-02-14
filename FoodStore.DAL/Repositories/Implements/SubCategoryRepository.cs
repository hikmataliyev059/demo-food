using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;

namespace FoodStore.DAL.Repositories.Implements;

public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
{
    public SubCategoryRepository(FoodStoreDbContext context) : base(context)
    {
    }
}