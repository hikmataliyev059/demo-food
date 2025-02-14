namespace FoodStore.BL.Helpers.DTOs.Blogs;

public record CommentGetDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string CommentDate { get; set; }
    public int ArticleId { get; set; }
}