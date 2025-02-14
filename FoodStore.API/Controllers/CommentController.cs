using FoodStore.BL.Helpers.DTOs.Blogs;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CommentCreateDto commentCreateDto)
    {
        var createdComment = await _commentService.AddCommentAsync(commentCreateDto);
        return CreatedAtAction(nameof(GetComments), new { articleId = commentCreateDto.ArticleId }, createdComment);
    }

    [HttpGet("{articleId}")]
    public async Task<IActionResult> GetComments(int articleId)
    {
        return Ok(await _commentService.GetCommentsByArticleIdAsync(articleId));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDto commentUpdateDto)
    {
        var updatedComment = await _commentService.UpdateCommentAsync(id, commentUpdateDto);
        return Ok(updatedComment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}