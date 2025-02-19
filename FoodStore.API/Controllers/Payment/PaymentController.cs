using FoodStore.BL.Helpers.DTOs.Payment;
using FoodStore.BL.Services.Implements.Stripe;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers.Payment;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly StripeService _stripeService;

    public PaymentController(StripeService stripeService)
    {
        _stripeService = stripeService;
    }

    [HttpPost("payment")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
    {
        var paymentIntent = await _stripeService.CreatePaymentIntent(paymentDto.Amount, "usd", paymentDto.Token);
        return Ok(new { clientSecret = paymentIntent.ClientSecret });
    }

    [HttpPost("confirm-payment")]
    public async Task<IActionResult> ConfirmPayment([FromBody] string paymentIntentId)
    {
        var paymentIntent = await _stripeService.ConfirmPaymentIntent(paymentIntentId);
        return Ok(paymentIntent);
    }
}