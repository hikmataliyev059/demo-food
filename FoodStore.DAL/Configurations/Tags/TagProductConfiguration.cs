using FoodStore.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStore.DAL.Configurations.Tags;

public class TagProductConfiguration : IEntityTypeConfiguration<TagProduct>
{
    public void Configure(EntityTypeBuilder<TagProduct> builder)
    {
        builder.HasKey(tp => tp.Id);

        builder.HasOne(tp => tp.Product)
            .WithMany(p => p.TagProducts)
            .HasForeignKey(tp => tp.ProductId);

        builder.HasOne(tp => tp.Tag)
            .WithMany(t => t.TagProducts)
            .HasForeignKey(tp => tp.TagId);
    }
}