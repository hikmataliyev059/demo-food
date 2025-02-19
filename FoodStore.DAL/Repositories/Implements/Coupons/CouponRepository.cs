using FoodStore.Core.Entities.Coupons;
using FoodStore.Core.Repositories.Interfaces.Coupons;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Repositories.Implements.Coupons;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
    public CouponRepository(FoodStoreDbContext context) : base(context)
    {
    }

    public async Task<Coupon?> GetCouponByCodeAsync(string code)
    {
        return await Table.FirstOrDefaultAsync(c => c.Code == code && c.IsActive && c.ExpiryDate > DateTime.UtcNow);
    }
}