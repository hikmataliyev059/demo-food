using System.Security.Claims;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpPost("add-to-wishlist/{productId}")]
    public async Task<IActionResult> AddToWishlist(int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _wishlistService.AddToWishlistAsync(productId, userId);
        return Ok("Product added to wishlist");
    }

    [HttpDelete("remove-from-wishlist/{productId}")]
    public async Task<IActionResult> RemoveFromWishlist(int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _wishlistService.RemoveFromWishlistAsync(productId, userId);
        return NoContent();
    }

    [HttpGet("get-all-wishlists")]
    public async Task<IActionResult> GetWishlist()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var wishlist = await _wishlistService.GetWishlistAsync(userId);
        return Ok(wishlist);
    }
}