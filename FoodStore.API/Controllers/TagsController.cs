using FoodStore.BL.Helpers.DTOs.Tag;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] TagCreateDto tagCreateDto)
    {
        await _tagService.CreateAsync(tagCreateDto);
        return StatusCode(201, tagCreateDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _tagService.GetByIdAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tags = await _tagService.GetAllAsync();
        return Ok(tags.Select(c => new { c.Id, c.Name, c.Slug }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] TagUpdateDto updateDto)
    {
        await _tagService.UpdateAsync(id, updateDto);
        return StatusCode(204, updateDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _tagService.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("SoftDelete/{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _tagService.SoftDeleteAsync(id);
        return NoContent();
    }
}