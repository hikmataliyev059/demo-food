using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.Common;

public class NegativeIdException : Exception, IBaseException
{
    public NegativeIdException() : base("Id cannot be zero or negative")
    {
    }

    public NegativeIdException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;
    public int StatusCode => 400;
}