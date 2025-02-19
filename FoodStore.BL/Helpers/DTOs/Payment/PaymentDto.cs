namespace FoodStore.BL.Helpers.DTOs.Payment;

public record PaymentDto
{
    public string Token { get; set; } // Stripe'dan gelen token
    public decimal Amount { get; set; }
}