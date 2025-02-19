using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Repositories.Interfaces.Common;

namespace FoodStore.Core.Repositories.Interfaces.Blogs;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<IEnumerable<Comment>> GetCommentsByArticleIdAsync(int articleId);
}