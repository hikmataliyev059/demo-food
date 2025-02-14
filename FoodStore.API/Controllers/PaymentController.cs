using FoodStore.BL.Helpers.DTOs.Payment;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("pay")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDTO paymentRequestDto)
    {
        var clientSecret = await _paymentService.ProcessPayment(paymentRequestDto);
        return Ok(new { clientSecret });
    }
}

