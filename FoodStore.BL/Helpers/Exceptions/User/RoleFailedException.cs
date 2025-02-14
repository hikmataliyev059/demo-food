using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.User;

public class RoleFailedException : Exception, IBaseException
{
    public RoleFailedException() : base("An error occurred while creating the role!")
    {
    }

    public RoleFailedException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 500;
}