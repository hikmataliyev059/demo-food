using FoodStore.Core.Entities.Common;
using FoodStore.Core.Entities.Products;

namespace FoodStore.Core.Entities.Blogs;

public class ArticleTag : BaseEntity
{
    public int ArticleId { get; set; }
    public Article Article { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}