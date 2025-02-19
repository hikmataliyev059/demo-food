using FoodStore.BL.Helpers.Email;

namespace FoodStore.BL.Services.Interfaces.Email;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}