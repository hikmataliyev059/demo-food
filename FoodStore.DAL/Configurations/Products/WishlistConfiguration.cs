// using FoodStore.Core.Entities.Wishlists;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace FoodStore.DAL.Configurations.Products;
//
// public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
// {
//     public void Configure(EntityTypeBuilder<Wishlist> builder)
//     {
//         builder.HasKey(w => w.Id);
//
//         builder.HasMany(w => w.ProductWishlists)
//             .WithOne(pw => pw.Wishlist)
//             .HasForeignKey(pw => pw.WishlistId);
//     }
// }