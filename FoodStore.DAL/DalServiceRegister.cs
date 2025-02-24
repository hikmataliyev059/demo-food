using FoodStore.Core.Repositories.Interfaces.Blogs;
using FoodStore.Core.Repositories.Interfaces.Cart;
using FoodStore.Core.Repositories.Interfaces.Categories;
using FoodStore.Core.Repositories.Interfaces.Contacts;
using FoodStore.Core.Repositories.Interfaces.Coupons;
using FoodStore.Core.Repositories.Interfaces.Products;
using FoodStore.Core.Repositories.Interfaces.Reviews;
using FoodStore.Core.Repositories.Interfaces.Wishlists;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Blogs;
using FoodStore.DAL.Repositories.Implements.Cart;
using FoodStore.DAL.Repositories.Implements.Categories;
using FoodStore.DAL.Repositories.Implements.Contacts;
using FoodStore.DAL.Repositories.Implements.Coupons;
using FoodStore.DAL.Repositories.Implements.Products;
using FoodStore.DAL.Repositories.Implements.Reviews;
using FoodStore.DAL.Repositories.Implements.Wishlists;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStore.DAL;

public static class DalServiceRegister
{
    public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FoodStoreDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Deploy"));
        });

        services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("Deploy")));
        services.AddHangfireServer();

        var googleCloudCredentialsPath = configuration["GoogleCloud:CredentialsPath"];

        if (!string.IsNullOrEmpty(googleCloudCredentialsPath))
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", googleCloudCredentialsPath);
        }
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
    }
}