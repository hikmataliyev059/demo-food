namespace FoodStore.BL.Helpers.DTOs.Auth;

public record TokenDto
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}