namespace FoodStore.BL.Helpers.DTOs.Category;

public record CategoryWithSubcategoriesDto
{
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<SubCategoryDto> Subcategories { get; set; }
}