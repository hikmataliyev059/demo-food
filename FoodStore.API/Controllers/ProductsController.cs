using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateDto createDto)
    {
        await _productService.CreateAsync(createDto);
        return StatusCode(201, createDto);
    }

    [HttpGet("single-product/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _productService.GetByIdAsync(id));
    }

    [HttpGet("all-products")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _productService.GetAllAsync());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDto updateDto)
    {
        await _productService.UpdateAsync(id, updateDto);
        return StatusCode(204, updateDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("SoftDelete/{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _productService.SoftDeleteAsync(id);
        return NoContent();
    }

    [HttpGet("Category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }

    [HttpGet("SubCategory/{subcategoryId}")]
    public async Task<IActionResult> GetBySubCategory(int subcategoryId)
    {
        var products = await _productService.GetProductsBySubCategoryAsync(subcategoryId);
        return Ok(products);
    }

    [HttpGet("TagIds")]
    public async Task<IActionResult> GetByTagIds([FromQuery] IEnumerable<int> tagIds)
    {
        var products = await _productService.GetProductsByTagIdsAsync(tagIds);
        return Ok(products);
    }

    [HttpPost("update-stock/")]
    public async Task<IActionResult> UpdateStock([FromForm] int productId, int quantity)
    {
        await _productService.UpdateStockAsync(productId, quantity);
        return Ok("Stock updated");
    }

    [HttpGet("{id}/edit")]
    public async Task<IActionResult> GetProductForUpdate(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }
}