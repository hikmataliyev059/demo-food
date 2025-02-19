using FoodStore.Core.Entities.Payments;
using Microsoft.Extensions.Options;
using Stripe;

namespace FoodStore.BL.Services.Implements.Stripe;

public class StripeService
{
    public StripeService(IOptions<StripeSettings> stripeSettings)
    {
        var stripeSettings1 = stripeSettings.Value;
        StripeConfiguration.ApiKey = stripeSettings1.SecretKey;
    }

    public async Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency, string paymentMethodId)
    {
        var paymentIntentOptions = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100), // Stripe, tutarı sent cinsinden alır
            Currency = currency,
            PaymentMethod = paymentMethodId, // Stripe'dan aldığımız payment method ID'si
            ConfirmationMethod = "manual",
            Confirm = true,
        };

        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions);

        return paymentIntent; // PaymentIntent nesnesi, işlem sonucunu döndürür
    }

    // Stripe'tan ödeme doğrulama işlemi
    public async Task<PaymentIntent> ConfirmPaymentIntent(string paymentIntentId)
    {
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = await paymentIntentService.ConfirmAsync(paymentIntentId);
        return paymentIntent;
    }
}