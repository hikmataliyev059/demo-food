using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Auth;
using FoodStore.Core.Entities;
using FoodStore.Core.Entities.User;

namespace FoodStore.BL.Helpers.Mapper;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<RegisterDto, AppUser>().ReverseMap();
    }
}