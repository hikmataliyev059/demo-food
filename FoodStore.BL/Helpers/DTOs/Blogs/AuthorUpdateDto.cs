namespace FoodStore.BL.Helpers.DTOs.Blogs;

public record AuthorUpdateDto
{
    public string Name { get; set; }
    public string Bio { get; set; }
    public string ImageUrl { get; set; }
}