using FoodStore.BL.Helpers.DTOs.Review;

namespace FoodStore.BL.Services.Interfaces.Review;

public interface IReviewService
{
    Task AddReviewAsync(ReviewDto reviewDto);
    Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId);
}