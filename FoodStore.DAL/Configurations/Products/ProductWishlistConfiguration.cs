// using FoodStore.Core.Entities;
// using FoodStore.Core.Entities.Products;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace FoodStore.DAL.Configurations.Products;
//
// public class ProductWishlistConfiguration : IEntityTypeConfiguration<ProductWishlist>
// {
//     public void Configure(EntityTypeBuilder<ProductWishlist> builder)
//     {
//         builder.HasKey(pw => new { pw.ProductId, pw.WishlistId, pw.UserId });
//
//         builder.HasOne(pw => pw.Product)
//             .WithMany(p => p.ProductWishlists)
//             .HasForeignKey(pw => pw.ProductId);
//
//         builder.HasOne(pw => pw.Wishlist)
//             .WithMany(p => p.ProductWishlists)
//             .HasForeignKey(pw => pw.WishlistId);
//     }
// }