using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Coupon;
using FoodStore.Core.Entities;
using FoodStore.Core.Entities.Coupons;

namespace FoodStore.BL.Helpers.Mapper;

public class CouponMapper : Profile
{
    public CouponMapper()
    {
        CreateMap<CouponDto, Coupon>().ReverseMap();
        CreateMap<CouponUpdateDto, Coupon>()
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();
    }
}