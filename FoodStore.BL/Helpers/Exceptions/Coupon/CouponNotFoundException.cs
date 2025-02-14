using FoodStore.BL.Helpers.Exceptions.Base;

namespace FoodStore.BL.Helpers.Exceptions.Coupon
{
    public class CouponNotFoundException : Exception, IBaseException
    {
        public CouponNotFoundException() : base("Coupon code not found or expired")
        {
        }

        public CouponNotFoundException(string? message) : base(message)
        {
        }

        public string ErrorMessage => Message;
        public int StatusCode => 404;
    }
}