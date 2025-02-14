using FoodStore.Core.Entities.Reviews;

namespace FoodStore.Core.Repositories.Interfaces;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
}