using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;

namespace FoodStore.DAL.Repositories.Implements;

public class ArticleRepository : GenericRepository<Article>, IArticleRepository
{
    public ArticleRepository(FoodStoreDbContext context) : base(context)
    {
    }
}