namespace FoodStore.BL.Helpers.DTOs.Auth;

public record TokenDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}