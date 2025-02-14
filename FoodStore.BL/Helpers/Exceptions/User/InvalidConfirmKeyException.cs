using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.User;

public class InvalidConfirmKeyException : Exception, IBaseException
{
    public InvalidConfirmKeyException() : base("Confirmkey duz deyil!")
    {
    }

    public InvalidConfirmKeyException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 400;
}