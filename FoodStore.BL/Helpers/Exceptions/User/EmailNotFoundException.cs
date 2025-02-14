using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.User;

public class EmailNotFoundException : Exception, IBaseException
{
    public EmailNotFoundException() : base("Email not verified!")
    {
    }

    public EmailNotFoundException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 404;
}