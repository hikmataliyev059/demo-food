using FoodStore.Core.Entities.Reviews;
using FoodStore.Core.Repositories.Interfaces.Common;

namespace FoodStore.Core.Repositories.Interfaces.Reviews;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
}