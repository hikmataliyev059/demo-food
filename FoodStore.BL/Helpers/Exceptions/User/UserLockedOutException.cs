using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.User;

public class UserLockedOutException : Exception, IBaseException
{
    public UserLockedOutException() : base("User is locked out")
    {
    }

    public UserLockedOutException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 423;
}