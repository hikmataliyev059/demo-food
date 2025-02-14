using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Blogs;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public DateTime CommentDate { get; set; }
    public int ArticleId { get; set; }
    public Article Article { get; set; }
}