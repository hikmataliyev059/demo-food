using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Auth;
using FoodStore.Core.Entities.User;

namespace FoodStore.BL.Helpers.Mapper.Auth;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<RegisterDto, AppUser>().ReverseMap();
    }
}