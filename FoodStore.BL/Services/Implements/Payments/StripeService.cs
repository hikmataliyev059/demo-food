using FoodStore.BL.Helpers.DTOs.Payment;
using FoodStore.BL.Helpers.Email;
using FoodStore.BL.Services.Interfaces.Email;
using FoodStore.Core.Entities.Payments;
using Microsoft.Extensions.Options;
using Stripe;

namespace FoodStore.BL.Services.Implements.Payments;

public class StripeService
{
    private readonly IMailService _mailService;

    public StripeService(IOptions<StripeSettings> stripeSettings, IMailService mailService)
    {
        _mailService = mailService;
        var stripeSettings1 = stripeSettings.Value;
        StripeConfiguration.ApiKey = stripeSettings1.SecretKey;
    }

    public async Task<PaymentIntent> CreatePaymentIntent(PaymentDto paymentDto)
    {
        var paymentMethodOptions = new PaymentMethodCreateOptions
        {
            Type = "card",
            Card = new PaymentMethodCardOptions
            {
                Token = paymentDto.Token
            },
            BillingDetails = new PaymentMethodBillingDetailsOptions
            {
                Name = paymentDto.BillingName,
                Email = paymentDto.BillingEmail,
                Phone = paymentDto.BillingPhone,
                Address = new AddressOptions
                {
                    Line1 = paymentDto.BillingAddress.Line1,
                    Line2 = paymentDto.BillingAddress.Line2,
                    City = paymentDto.BillingAddress.City,
                    State = paymentDto.BillingAddress.State,
                    PostalCode = paymentDto.BillingAddress.PostalCode,
                    Country = paymentDto.BillingAddress.Country
                }
            }
        };

        var paymentMethodService = new PaymentMethodService();
        var paymentMethod = await paymentMethodService.CreateAsync(paymentMethodOptions);

        var paymentIntentOptions = new PaymentIntentCreateOptions
        {
            Amount = (long)(paymentDto.Amount * 100),
            Currency = "usd",
            PaymentMethod = paymentMethod.Id,
            ReturnUrl = "https://grofferecom.netlify.app/paymentsucceed",
            Confirm = true
        };

        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions);

        switch (paymentIntent.Status)
        {
            case "requires_confirmation":
                paymentIntent = await paymentIntentService.ConfirmAsync(paymentIntent.Id);
                break;
            case "succeeded":
            {
                var mailRequest = new MailRequest
                {
                    ToEmail = paymentDto.BillingEmail,
                    Subject = "Payment Successful",
                    Body = $"Dear {paymentDto.BillingName}, your payment of {paymentDto.Amount} USD was successful"
                };

                await _mailService.SendEmailAsync(mailRequest);
                Console.WriteLine("Payment has already been completed successfully");
                break;
            }
        }

        return paymentIntent;
    }
}