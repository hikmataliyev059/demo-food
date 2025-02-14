using FoodStore.BL.Helpers.DTOs.Tag;

namespace FoodStore.BL.Services.Interfaces;

public interface ITagService
{
    Task<TagGetDto?> GetByIdAsync(int id);

    Task<IEnumerable<TagGetDto>> GetAllAsync();

    Task CreateAsync(TagCreateDto tagCreateDto);

    Task UpdateAsync(int id, TagUpdateDto tagUpdateDto);

    Task DeleteAsync(int id);

    Task SoftDeleteAsync(int id);
}