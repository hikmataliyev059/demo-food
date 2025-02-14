using FoodStore.Core.Entities.Reviews;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Repositories.Implements;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(FoodStoreDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId)
    {
        return await Table
            .Where(r => r.ProductId == productId && !r.IsDeleted)
            .Include(r => r.Product)
            .ToListAsync();
    }
}