using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;

namespace FoodStore.DAL.Repositories.Implements;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    public AuthorRepository(FoodStoreDbContext context) : base(context)
    {
    }
}