namespace FoodStore.BL.Helpers.DTOs.Product;

public record ProductCreateDto
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string SKU { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public decimal Discount { get; set; }
    public int? SubCategoryId { get; set; }
    public List<int> TagIds { get; set; } = new List<int>();
    public string PrimaryImageUrl { get; set; } = string.Empty;
    public List<string> SubImageUrls { get; set; } = new List<string>();
}