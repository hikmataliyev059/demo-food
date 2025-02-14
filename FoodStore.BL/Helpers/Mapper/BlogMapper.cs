using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Blogs;
using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Entities.Products;

namespace FoodStore.BL.Helpers.Mapper
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>()
                .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();
            CreateMap<AuthorGetDto, Author>().ReverseMap();

            CreateMap<ArticleCreateDto, Article>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.ArticleTags,
                    opt => opt.MapFrom(src => src.TagIds.Select(tagId => new ArticleTag { TagId = tagId }).ToList()))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ReverseMap();

            CreateMap<ArticleUpdateDto, Article>()
                .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();

            CreateMap<ArticleGetDto, Article>()
                .ForMember(dest => dest.ArticleTags, opt => opt.MapFrom(src =>
                    src.TagNames.Select(tagName => new ArticleTag { Tag = new Tag { Name = tagName } }))).ReverseMap();

            CreateMap<Article, ArticleGetDto>()
                .ForMember(dest => dest.PublishDate,
                    opt => opt.MapFrom(src => src.PublishDate.AddHours(4).ToString("MMMM dd, yyyy")))
                .ForMember(dest => dest.TagNames, opt => opt.MapFrom(src => src.ArticleTags != null
                    ? src.ArticleTags.Select(tag => tag.Tag.Name).ToList()
                    : new List<string>()))
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.ArticleTags != null
                    ? src.ArticleTags.Select(tag => tag.TagId).ToList()
                    : new List<int>())).ReverseMap();

            CreateMap<CommentCreateDto, Comment>()
                .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();

            CreateMap<CommentUpdateDto, Comment>()
                .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ReverseMap();

            CreateMap<Comment, CommentGetDto>()
                .ForMember(dest => dest.CommentDate,
                    opt => opt.MapFrom(src => src.CommentDate.AddHours(4).ToString("MMMM dd, yyyy 'at' hh:mm tt")))
                .ReverseMap();
        }
    }
}