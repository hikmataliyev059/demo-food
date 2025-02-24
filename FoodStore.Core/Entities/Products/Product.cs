using FoodStore.Core.Entities.Cart;
using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Entities.Common;
using FoodStore.Core.Entities.Reviews;
using FoodStore.Core.Entities.Wish;

namespace FoodStore.Core.Entities.Products;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string SKU { get; set; }
    public int StockQuantity { get; set; }
    public decimal Discount { get; set; }
    public decimal DiscountedPrice => Discount > 0 ? Price - Price * Discount / 100 : Price;
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int? SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
    public ICollection<TagProduct> TagProducts { get; set; } = new List<TagProduct>();
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}