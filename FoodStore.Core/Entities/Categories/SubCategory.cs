using FoodStore.Core.Entities.Common;
using FoodStore.Core.Entities.Products;

namespace FoodStore.Core.Entities.Categories;

public class SubCategory : BaseEntity
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}