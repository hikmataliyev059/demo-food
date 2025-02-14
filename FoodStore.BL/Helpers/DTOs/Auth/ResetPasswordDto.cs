namespace FoodStore.BL.Helpers.DTOs.Auth;

public record ResetPasswordDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}