namespace FoodStore.BL.Helpers.DTOs.Auth;

public record SubmitRegistrationDto
{
    public string Email { get; set; }
    public string ConfirmKey { get; set; }
}