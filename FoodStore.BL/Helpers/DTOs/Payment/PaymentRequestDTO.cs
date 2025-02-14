namespace FoodStore.BL.Helpers.DTOs.Payment;

public record PaymentRequestDTO
{
    public string Email { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string Cvc { get; set; }
    public string NameOnCard { get; set; }
    public string CountryOrRegion { get; set; }
    public string PostalCode { get; set; }
    public decimal Amount { get; set; }
}