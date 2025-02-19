using FoodStore.BL.Helpers.DTOs.Blogs;

namespace FoodStore.BL.Services.Interfaces.Blogs;

public interface IArticleService
{
    Task<ArticleGetDto> CreateArticleAsync(ArticleCreateDto articleCreateDto);
    
    Task<ArticleGetDto> GetArticleByIdAsync(int id);
    
    Task<IEnumerable<ArticleGetDto>> GetAllArticlesAsync();
    
    Task<ArticleGetDto> UpdateArticleAsync(int id, ArticleUpdateDto articleUpdateDto);
    
    Task DeleteArticleAsync(int id);
}