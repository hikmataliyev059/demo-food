using FoodStore.Core.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStore.DAL.Configurations.Blogs;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title).IsRequired().HasMaxLength(200);

        builder.Property(a => a.Content).IsRequired();

        builder.Property(a => a.ImageUrl).HasMaxLength(500);

        builder.Property(a => a.PublishDate).IsRequired();

        builder.HasOne(a => a.Category).WithMany().HasForeignKey(a => a.CategoryId);
    }
}