namespace FoodStore.BL.Helpers.DTOs.Tag;

public record TagCreateDto
{
    public string Name { get; set; }
    public string Slug { get; set; }
}