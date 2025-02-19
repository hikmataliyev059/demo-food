using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Category;
using FoodStore.BL.Helpers.Extensions.Slug;
using FoodStore.Core.Entities.Categories;

namespace FoodStore.BL.Helpers.Mapper.Categories;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CategoryCreateDto, Category>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => SlugHelper.CreateSlug(src.Name))).ReverseMap();
        CreateMap<CategoryUpdateDto, Category>()
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();
        CreateMap<CategoryGetDto, Category>().ReverseMap();

        CreateMap<SubCategoryCreateDto, SubCategory>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => SlugHelper.CreateSlug(src.Name))).ReverseMap();
        CreateMap<SubCategoryUpdateDto, SubCategory>()
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();

        CreateMap<SubCategory, SubCategoryGetDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ReverseMap();

        CreateMap<SubCategory, SubCategoryDto>()
            .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Category, CategoryWithSubcategoriesDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.SubCategories));
    }
}