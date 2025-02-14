using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Repositories.Implements;

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