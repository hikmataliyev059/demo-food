using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Repositories.Interfaces.Blogs;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Repositories.Implements.Blogs;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(FoodStoreDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Comment>> GetCommentsByArticleIdAsync(int articleId)
    {
        var comments = await Table.Where(c => c.ArticleId == articleId).ToListAsync();
        return comments;
    }
}