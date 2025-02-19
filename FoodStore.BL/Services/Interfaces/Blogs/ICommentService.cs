using FoodStore.BL.Helpers.DTOs.Blogs;

namespace FoodStore.BL.Services.Interfaces.Blogs;

public interface ICommentService
{
    Task<CommentGetDto> AddCommentAsync(CommentCreateDto commentCreateDto);
    
    Task<IEnumerable<CommentGetDto>> GetCommentsByArticleIdAsync(int articleId);
    
    Task<CommentGetDto> UpdateCommentAsync(int id, CommentUpdateDto commentUpdateDto);
    
    Task DeleteCommentAsync(int id);
}