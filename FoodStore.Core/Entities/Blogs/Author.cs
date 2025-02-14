using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Blogs;

public class Author : BaseEntity
{
    public string Name { get; set; }
    public string Bio { get; set; }
    public string ImageUrl { get; set; }
}