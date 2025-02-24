using System.Reflection;
using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Entities.Cart;
using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Entities.Contacts;
using FoodStore.Core.Entities.Coupons;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Entities.Reviews;
using FoodStore.Core.Entities.User;
using FoodStore.Core.Entities.Wish;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Context;

public class FoodStoreDbContext : IdentityDbContext<AppUser>
{
    public FoodStoreDbContext(DbContextOptions<FoodStoreDbContext> options) : base(options)
    {
    }
    
    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<TagProduct> TagProducts { get; set; }

    public DbSet<SubCategory> SubCategories { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Coupon> Coupons { get; set; }

    public DbSet<Article> Articles { get; set; }

    public DbSet<ArticleTag> ArticleTags { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Wishlist> Wishlists { get; set; }

    public DbSet<Contact> Contacts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}