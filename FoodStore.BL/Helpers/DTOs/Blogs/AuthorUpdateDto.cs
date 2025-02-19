using Microsoft.AspNetCore.Http;

namespace FoodStore.BL.Helpers.DTOs.Blogs;

public record AuthorUpdateDto
{
    public string Name { get; set; }
    public string Bio { get; set; }
    public IFormFile Image { get; set; }
}