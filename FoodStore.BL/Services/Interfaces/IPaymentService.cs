using FoodStore.BL.Helpers.DTOs.Payment;

namespace FoodStore.BL.Services.Interfaces;

public interface IPaymentService
{
    Task<string> ProcessPayment(PaymentRequestDTO paymentRequestDto);
}