using FoodStore.BL.Helpers.Email;

namespace FoodStore.BL.Services.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}