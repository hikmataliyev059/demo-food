using System.Security.Claims;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("add-to-cart/{productId}")]
    public async Task<IActionResult> AddToCart(int productId, [FromForm] int quantity)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _cartService.AddToCartAsync(productId, userId, quantity);
        return Ok("Product added to cart");
    }

    [HttpGet("cart")]
    public async Task<IActionResult> GetCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var cart = await _cartService.GetCartAsync(userId);
        return Ok(cart);
    }

    [HttpPut("update-cart/{productId}")]
    public async Task<IActionResult> UpdateCart(int productId, [FromForm] int quantity)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _cartService.UpdateCartAsync(productId, userId, quantity);
        return Ok("Cart updated");
    }

    [HttpDelete("remove-from-cart/{productId}")]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _cartService.RemoveFromCartAsync(productId, userId);
        return NoContent();
    }

    [HttpDelete("clear-cart")]
    public async Task<IActionResult> ClearCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _cartService.ClearCartAsync(userId);
        return NoContent();
    }
}