namespace FoodStore.BL.Helpers.DTOs.Category;

public record SubCategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}