using System.Text.Json;
using FoodStore.BL.Helpers.Exceptions.Base;
using Microsoft.AspNetCore.Diagnostics;

namespace FoodStore.API.Utils;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (feature?.Error is IBaseException ex)
                {
                    context.Response.StatusCode = ex.StatusCode;

                    var response = new
                    {
                        Message = ex.ErrorMessage, ex.StatusCode
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var response = new
                    {
                        Message = "An unexpected error occurred",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });
        });
    }
}