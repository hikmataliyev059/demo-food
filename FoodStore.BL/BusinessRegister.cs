using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using FoodStore.BL.Helpers.Email;
using FoodStore.BL.Services.Implements;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Entities.User;
using FoodStore.Core.Enums;
using FoodStore.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using CouponService = FoodStore.BL.Services.Implements.CouponService;
using ProductService = FoodStore.BL.Services.Implements.ProductService;
using ReviewService = FoodStore.BL.Services.Implements.ReviewService;

namespace FoodStore.BL;

public static class BusinessRegister
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BusinessRegister));
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUploadFileService, UploadFileService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<ICartService, CartService>();
        services.AddControllers().AddFluentValidation(cfg =>
        {
            cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    public static void AddStripe(this IServiceCollection services, IConfiguration configuration)
    {
        var stripeSettings = configuration.GetSection("StripeSettings");

        StripeConfiguration.ApiKey = stripeSettings["SecretKey"];
    }

    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.SignIn.RequireConfirmedEmail = false;
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.AllowedForNewUsers = true;
            opt.Password.RequiredLength = 4;
        }).AddEntityFrameworkStores<FoodStoreDbContext>().AddDefaultTokenProviders();
    }

    public static void ConfigureMailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IMailService, MailService>();
    }

    public static void GenerateJwtToken(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecurityKey"]!))
                };
            });
    }

    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JWT"
                        },
                        Scheme = "bearer",
                        Name = "JWT",
                        In = ParameterLocation.Header,
                    },
                    new string[] { }
                }
            });
        });
    }

    public static void UseSeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            CreateRoles(roleManager).Wait();
            CreateAdmin(userManager, configuration).Wait();
        }
    }

    private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
    {
        int res = await roleManager.Roles.CountAsync();

        if (res == 0)
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                await roleManager.CreateAsync(new IdentityRole(role.ToString()!));
            }
        }
    }

    private static async Task CreateAdmin(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        var adminSettings = configuration.GetSection("AdminSettings");

        var adminUsername = adminSettings["Username"];
        var adminFirstName = adminSettings["FirstName"];
        var adminLastName = adminSettings["LastName"];
        var adminEmail = adminSettings["Email"];
        var adminPassword = adminSettings["Password"];

        if (!await userManager.Users.AnyAsync(x => x.UserName == adminUsername))
        {
            AppUser user = new AppUser
            {
                UserName = adminUsername,
                FirstName = adminFirstName!,
                LastName = adminLastName!,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, adminPassword!);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }
        }
    }
}