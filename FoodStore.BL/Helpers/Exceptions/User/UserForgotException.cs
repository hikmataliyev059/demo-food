using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.User;

public class UserForgotException : Exception, IBaseException
{
    public UserForgotException() : base("Email does not exist")
    {
    }

    public UserForgotException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 400;
}