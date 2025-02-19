using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Review;
using FoodStore.Core.Entities.Reviews;

namespace FoodStore.BL.Helpers.Mapper.Reviews;

public class ReviewMapper : Profile
{
    public ReviewMapper()
    {
        CreateMap<Review, ReviewDto>().ReverseMap();
    }
}