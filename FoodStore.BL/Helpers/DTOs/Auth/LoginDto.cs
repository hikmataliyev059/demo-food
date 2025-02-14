namespace FoodStore.BL.Helpers.DTOs.Auth;

public record LoginDto
{
    public string EmailOrUsername { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}