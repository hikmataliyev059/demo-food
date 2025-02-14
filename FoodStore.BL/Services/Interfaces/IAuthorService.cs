using FoodStore.BL.Helpers.DTOs.Blogs;

namespace FoodStore.BL.Services.Interfaces;

public interface IAuthorService
{
    Task<AuthorGetDto> GetAuthorByIdAsync(int id);
    Task<IEnumerable<AuthorGetDto>> GetAllAuthorsAsync();
    Task<AuthorGetDto> CreateAuthorAsync(AuthorCreateDto authorCreateDto);
    Task<AuthorGetDto> UpdateAuthorAsync(int id, AuthorUpdateDto authorUpdateDto);
    Task DeleteAuthorAsync(int id);
}