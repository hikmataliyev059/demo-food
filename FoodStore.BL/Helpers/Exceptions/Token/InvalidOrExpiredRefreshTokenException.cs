using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.Token;

public class InvalidOrExpiredRefreshTokenException : Exception, IBaseException
{
    public InvalidOrExpiredRefreshTokenException() : base("Invalid or expired refresh token")
    {
    }

    public InvalidOrExpiredRefreshTokenException(string? message) : base(message)
    {
    }

    public string ErrorMessage => Message;

    public int StatusCode => 401;
}