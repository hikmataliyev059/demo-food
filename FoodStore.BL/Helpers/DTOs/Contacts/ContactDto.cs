namespace FoodStore.BL.Helpers.DTOs.Contacts;

public record ContactDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}