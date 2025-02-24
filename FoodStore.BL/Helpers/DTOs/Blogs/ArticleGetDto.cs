namespace FoodStore.BL.Helpers.DTOs.Blogs;

public record ArticleGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public string PublishDate { get; set; }
    public List<int> TagIds { get; set; } = [];
    public List<string> TagNames { get; set; } = [];
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public string AuthorImageUrl { get; set; }
}