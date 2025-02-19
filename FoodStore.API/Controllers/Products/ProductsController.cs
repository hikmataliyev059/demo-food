using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Services.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers.Products;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
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
        return Ok();
    }

    [HttpGet("single-product/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _productService.GetByIdAsync(id));
    }

    [HttpGet("all-products")]
    [AllowAnonymous]
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
    [AllowAnonymous]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }

    [HttpGet("SubCategory/{subcategoryId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBySubCategory(int subcategoryId)
    {
        var products = await _productService.GetProductsBySubCategoryAsync(subcategoryId);
        return Ok(products);
    }

    [HttpGet("TagIds")]
    [AllowAnonymous]
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