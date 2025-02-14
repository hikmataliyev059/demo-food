using FoodStore.BL.Helpers.Exceptions.Base;
using FoodStore.Core.Entities.Common;

namespace FoodStore.BL.Helpers.Exceptions.Common;

public class NotFoundException<T> : Exception, IBaseException where T : BaseEntity, new()
{
    public NotFoundException() : base($"{typeof(T).Name} not found")
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 404;
}