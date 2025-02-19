using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Repositories.Interfaces.Blogs;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;

namespace FoodStore.DAL.Repositories.Implements.Blogs;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    public AuthorRepository(FoodStoreDbContext context) : base(context)
    {
    }
}