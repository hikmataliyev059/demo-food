using FoodStore.Core.Entities.Products;
using FoodStore.Core.Repositories.Interfaces.Products;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;

namespace FoodStore.DAL.Repositories.Implements.Products;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(FoodStoreDbContext context) : base(context)
    {
    }
}