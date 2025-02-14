using FoodStore.Core.Entities.Blogs;

namespace FoodStore.Core.Repositories.Interfaces;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<IEnumerable<Comment>> GetCommentsByArticleIdAsync(int articleId);
}