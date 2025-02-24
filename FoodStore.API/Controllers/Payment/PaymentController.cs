using FoodStore.BL.Helpers.DTOs.Payment;
using FoodStore.BL.Services.Implements.Payments;
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
        var paymentIntent = await _stripeService.CreatePaymentIntent(paymentDto);
        return Ok(new { clientSecret = paymentIntent.ClientSecret });
    }
}