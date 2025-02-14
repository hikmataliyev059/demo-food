using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.Common;

public class NullException<T> : Exception, IBaseException where T : class
{
    public NullException() : base($"{typeof(T).Name} cannot be null")
    {
    }

    public NullException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 400;
}