using FoodStore.Core.Entities.Common;
using FoodStore.Core.Entities.Products;

namespace FoodStore.Core.Entities.Categories;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<SubCategory> SubCategories { get; set; }
}