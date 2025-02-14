using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.Common;

public class AlreadyExistsException<T> : Exception, IBaseException where T : class
{
    public AlreadyExistsException() : base($"{typeof(T).Name} already exists")
    {
    }

    public AlreadyExistsException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 409;
}