namespace FoodStore.BL.Helpers.DTOs.Review;

public record ReviewDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}