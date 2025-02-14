using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Extensions.Slug;
using FoodStore.Core.Entities;
using FoodStore.Core.Entities.Products;

namespace FoodStore.BL.Helpers.Mapper;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductGetDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.Name))
            .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.TagProducts.Select(tp => tp.TagId).ToList()))
            .ForMember(dest => dest.TagNames,
                opt => opt.MapFrom(src => src.TagProducts.Select(tp => tp.Tag.Name).ToList()))
            .ForMember(dest => dest.PrimaryImageUrl, opt =>
                opt.MapFrom(src =>
                    src.ProductImages.FirstOrDefault(p => p.Primary) != null
                        ? src.ProductImages.FirstOrDefault(p => p.Primary)!.ImgUrl
                        : null))
            .ForMember(dest => dest.SubImageUrls, opt =>
                opt.MapFrom(src => src.ProductImages.Where(p => !p.Primary).Select(p => p.ImgUrl).ToList()))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.DiscountedPrice, opt => opt.MapFrom(src => src.DiscountedPrice));


        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => SlugHelper.CreateSlug(src.Name)))
            .ForMember(dest => dest.TagProducts,
                opt => opt.MapFrom(src => src.TagIds.Select(id => new TagProduct { TagId = id }).ToList()))
            .ForMember(dest => dest.ProductImages,
                opt => opt.MapFrom(src =>
                    src.SubImageUrls.Select(url => new ProductImage { ImgUrl = url, Primary = false }).ToList()))
            .ForMember(dest => dest.ProductImages,
                opt => opt.MapFrom((src, dest) =>
                    new List<ProductImage> { new ProductImage { ImgUrl = src.PrimaryImageUrl, Primary = true } }
                        .Concat(src.SubImageUrls.Select(url => new ProductImage { ImgUrl = url, Primary = false }))
                        .ToList()));


        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.TagProducts,
                opt => opt.MapFrom(src => src.TagIds.Select(id => new TagProduct { TagId = id }).ToList()))
            .ForMember(dest => dest.ProductImages,
                opt => opt.MapFrom((src, dest) =>
                {
                    var productImages = new List<ProductImage>();

                    if (!string.IsNullOrEmpty(src.PrimaryImageUrl))
                    {
                        productImages.Add(new ProductImage
                        {
                            ImgUrl = src.PrimaryImageUrl,
                            Primary = true
                        });
                    }

                    if (src.SubImageUrls != null && src.SubImageUrls.Any())
                    {
                        productImages.AddRange(src.SubImageUrls.Select(url => new ProductImage
                        {
                            ImgUrl = url,
                            Primary = false
                        }));
                    }

                    return productImages;
                })).ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();

        CreateMap<Product, ProductUpdateDto>()
            .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.TagProducts.Select(tp => tp.TagId).ToList()))
            .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src =>
                src.ProductImages.FirstOrDefault(p => p.Primary) != null
                    ? src.ProductImages.FirstOrDefault(p => p.Primary).ImgUrl
                    : null))
            .ForMember(dest => dest.SubImageUrls,
                opt => opt.MapFrom(src => src.ProductImages.Where(p => !p.Primary).Select(pi => pi.ImgUrl).ToList()))
            .ReverseMap();
    }
}