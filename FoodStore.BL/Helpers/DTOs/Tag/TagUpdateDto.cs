namespace FoodStore.BL.Helpers.DTOs.Tag;

public record TagUpdateDto
{
    public string Name { get; set; }
    public string Slug { get; set; }
}