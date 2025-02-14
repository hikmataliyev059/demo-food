using FoodStore.BL.Helpers.DTOs.Blogs;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleGetDto>> GetArticleByIdAsync(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        return Ok(article);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleGetDto>>> GetAllArticlesAsync()
    {
        return Ok(await _articleService.GetAllArticlesAsync());
    }

    [HttpPost]
    public async Task<ActionResult<ArticleGetDto>> CreateArticleAsync([FromBody] ArticleCreateDto articleCreateDto)
    {
        var createdArticle = await _articleService.CreateArticleAsync(articleCreateDto);
        return StatusCode(201, createdArticle);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ArticleGetDto>> UpdateArticleAsync(int id, [FromBody] ArticleUpdateDto articleUpdate)
    {
        var updatedArticle = await _articleService.UpdateArticleAsync(id, articleUpdate);
        return Ok(updatedArticle);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticleAsync(int id)
    {
        await _articleService.DeleteArticleAsync(id);
        return NoContent();
    }
}