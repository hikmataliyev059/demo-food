using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Tag;
using FoodStore.BL.Helpers.Extensions.Slug;
using FoodStore.Core.Entities;
using FoodStore.Core.Entities.Products;

namespace FoodStore.BL.Helpers.Mapper;

public class TagMapper : Profile
{
    public TagMapper()
    {
        CreateMap<TagCreateDto, Tag>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => SlugHelper.CreateSlug(src.Name))).ReverseMap();
        CreateMap<TagUpdateDto, Tag>()
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();
        CreateMap<TagGetDto, Tag>().ReverseMap();
    }
}