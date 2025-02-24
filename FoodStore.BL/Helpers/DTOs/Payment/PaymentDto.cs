namespace FoodStore.BL.Helpers.DTOs.Payment;

public record PaymentDto
{
    public string Token { get; set; }
    public decimal Amount { get; set; }
    public string BillingName { get; set; }
    public string BillingEmail { get; set; }
    public string BillingPhone { get; set; }
    public BillingAddressDto BillingAddress { get; set; }
}