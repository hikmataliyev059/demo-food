namespace FoodStore.BL.Helpers.DTOs.Blogs;

public record ArticleUpdateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; }
    public int AuthorId { get; set; }
}