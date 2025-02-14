using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.User;

public class UserRegisterException : Exception, IBaseException
{
    public UserRegisterException() : base("Registration Failed")
    {
    }

    public UserRegisterException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 404;
}