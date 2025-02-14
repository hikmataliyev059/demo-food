namespace FoodStore.BL.Helpers.DTOs.Category;

public record CategoryUpdateDto
{
    public string Name { get; set; }
    public string Slug { get; set; }
}