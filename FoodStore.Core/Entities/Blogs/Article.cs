using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Blogs;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime PublishDate { get; set; }
    public string AuthorId { get; set; }
    public Author Author { get; set; }
    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    public ICollection<Comment> Comments { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}