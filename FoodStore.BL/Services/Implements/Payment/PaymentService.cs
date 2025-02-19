// using FoodStore.BL.Helpers.Email;
// using Stripe;
//
// namespace FoodStore.BL.Services.Implements.Payment;
//
// public class PaymentService : IPaymentService
// {
//     private readonly IOrderRepository _orderRepository;
//     private readonly IPaymentRepository _paymentRepository;
//     private readonly IMailService _mailService;
//
//     public PaymentService(IOrderRepository orderRepository, IPaymentRepository paymentRepository,
//         IMailService mailService)
//     {
//         _orderRepository = orderRepository;
//         _paymentRepository = paymentRepository;
//         _mailService = mailService;
//     }
//
//     public async Task<string> ProcessPayment(PaymentRequestDTO paymentRequestDto)
//     {
//         var options = new PaymentIntentCreateOptions
//         {
//             Amount = (long)(paymentRequestDto.Amount * 100),
//             Currency = "usd",
//             ReceiptEmail = paymentRequestDto.Email
//         };
//
//         var service = new PaymentIntentService();
//         PaymentIntent paymentIntent = await service.CreateAsync(options);
//
//         var order = new Order
//         {
//             Amount = paymentRequestDto.Amount,
//             OrderStatus = OrderStatus.Pending,
//             PaymentIntentId = paymentIntent.Id,
//             ReceiptEmail = paymentRequestDto.Email
//         };
//
//         var payment = new Payment
//         {
//             OrderId = order.Id,
//             Amount = paymentRequestDto.Amount,
//             PaymentMethod = "Card",
//             PaymentDate = DateTime.UtcNow
//         };
//
//         var mailRequest = new MailRequest
//         {
//             ToEmail = paymentRequestDto.Email,
//             Subject = "Ödənişiniz Tamamlandı",
//             Body =
//                 $"Ödənişiniz {paymentRequestDto.Amount} USD məbləği ilə uğurla tamamlanıb. Ödəniş ID: {paymentIntent.Id}."
//         };
//         await _mailService.SendEmailAsync(mailRequest);
//         
//         await _orderRepository.AddAsync(order);
//         await _paymentRepository.AddAsync(payment);
//         await _orderRepository.SaveChangesAsync();
//         await _paymentRepository.SaveChangesAsync();
//
//         return paymentIntent.ClientSecret;
//     }
// }