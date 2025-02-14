namespace FoodStore.BL.Helpers.DTOs.Category;

public record CategoryCreateDto
{
    public string Name { get; set; }
    public string Slug { get; set; }
}