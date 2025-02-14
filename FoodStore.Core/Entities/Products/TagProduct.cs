using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Products;

public class TagProduct : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}