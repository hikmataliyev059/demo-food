using FoodStore.BL.Helpers.DTOs.Category;
using FoodStore.BL.Services.Interfaces.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers.Categories;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class SubCategoriesController : ControllerBase
{
    private readonly ISubCategoryService _subCategoryService;

    public SubCategoriesController(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SubCategoryCreateDto createDto)
    {
        await _subCategoryService.CreateAsync(createDto);
        return StatusCode(201, createDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SubCategoryUpdateDto updateDto)
    {
        await _subCategoryService.UpdateAsync(id, updateDto);
        return StatusCode(204, updateDto);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _subCategoryService.GetByIdAsync(id));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _subCategoryService.GetAllAsync();
        return Ok(categories.Select(c => new { c.Id, c.Name, c.Slug }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _subCategoryService.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("SoftDelete/{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _subCategoryService.SoftDeleteAsync(id);
        return NoContent();
    }
}