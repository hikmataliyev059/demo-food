using FoodStore.Core.Entities.Common;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Entities.User;

namespace FoodStore.Core.Entities.Wishlists;

public class Wishlist : BaseEntity
{
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}