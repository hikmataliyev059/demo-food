using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Reviews;

public class Review : BaseEntity
{
    public int ProductId { get; set; }
    public Products.Product Product { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}