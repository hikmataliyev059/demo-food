using FoodStore.BL.Helpers.DTOs.Review;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
    {
        await _reviewService.AddReviewAsync(reviewDto);
        return Ok(new { message = "Review added successfully." });
    }

    [HttpGet("get/{productId}")]
    public async Task<IActionResult> GetReviews(int productId)
    {
        var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
        return Ok(reviews);
    }
}