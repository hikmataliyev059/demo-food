using FoodStore.BL.Helpers.DTOs.Blogs;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorGetDto>> GetAuthorByIdAsync(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return Ok(author);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorGetDto>>> GetAllAuthorsAsync()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorGetDto>> CreateAuthorAsync([FromBody] AuthorCreateDto authorCreateDto)
    {
        var createdAuthor = await _authorService.CreateAuthorAsync(authorCreateDto);
        return StatusCode(201, createdAuthor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AuthorGetDto>> UpdateAuthorAsync(int id, [FromBody] AuthorUpdateDto authorUpdateDto)
    {
        var updatedAuthor = await _authorService.UpdateAuthorAsync(id, authorUpdateDto);
        return Ok(updatedAuthor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorAsync(int id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return NoContent();
    }
}