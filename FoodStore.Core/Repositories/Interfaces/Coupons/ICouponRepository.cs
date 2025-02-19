using FoodStore.Core.Entities.Coupons;
using FoodStore.Core.Repositories.Interfaces.Common;

namespace FoodStore.Core.Repositories.Interfaces.Coupons;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<Coupon?> GetCouponByCodeAsync(string code);
}