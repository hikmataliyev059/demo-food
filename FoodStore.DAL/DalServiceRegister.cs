using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements;
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
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
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