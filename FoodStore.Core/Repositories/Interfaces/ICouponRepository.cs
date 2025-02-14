using FoodStore.Core.Entities.Coupons;

namespace FoodStore.Core.Repositories.Interfaces;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<Coupon?> GetCouponByCodeAsync(string code);
}