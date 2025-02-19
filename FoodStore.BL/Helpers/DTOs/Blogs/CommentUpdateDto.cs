namespace FoodStore.BL.Helpers.DTOs.Blogs;

public record CommentUpdateDto
{
    public string Content { get; set; }  
    public string UserName { get; set; } 
    public string UserEmail { get; set; }
}