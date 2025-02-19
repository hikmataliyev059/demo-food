using FoodStore.Core.Entities.Reviews;
using FoodStore.Core.Repositories.Interfaces.Reviews;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements.Common;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Repositories.Implements.Reviews;

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