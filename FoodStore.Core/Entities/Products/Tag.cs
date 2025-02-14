using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Products;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public ICollection<TagProduct> TagProducts { get; set; }
    public ICollection<ArticleTag> ArticleTags { get; set; }
}