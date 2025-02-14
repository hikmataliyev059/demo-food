using FoodStore.API.Utils;
using FoodStore.BL;
using FoodStore.DAL;

namespace FoodStore.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerDocumentation();

        builder.Services.AddBusinessServices();
        builder.Services.AddConfiguration(builder.Configuration);
        builder.Services.AddStripe(builder.Configuration);
        builder.Services.ConfigureMailServices(builder.Configuration);
        builder.Services.AddRepositories();

        builder.Services.AddIdentityServices();

        builder.Services.GenerateJwtToken(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.ConfigureExceptionHandler();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseSeedData();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}