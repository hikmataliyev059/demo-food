using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Review;
using FoodStore.Core.Entities;
using FoodStore.Core.Entities.Reviews;

namespace FoodStore.BL.Helpers.Mapper;

public class ReviewMapper : Profile
{
    public ReviewMapper()
    {
        CreateMap<Review, ReviewDto>().ReverseMap();
    }
}