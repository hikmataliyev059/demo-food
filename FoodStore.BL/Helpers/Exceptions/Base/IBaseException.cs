namespace FoodStore.BL.Helpers.Exceptions.Base;

public interface IBaseException
{
    string ErrorMessage { get; }
    int StatusCode { get; }
}