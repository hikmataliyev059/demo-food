using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.User;

public class UserLoginFaildException : Exception, IBaseException
{
    public UserLoginFaildException() : base("Username or password is incorrect")
    {
    }

    public UserLoginFaildException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 404;
}