using FoodStore.BL.Helpers.DTOs.Category;

namespace FoodStore.BL.Services.Interfaces;

public interface ISubCategoryService
{
    Task<SubCategoryGetDto?> GetByIdAsync(int id);

    Task<IEnumerable<SubCategoryGetDto>> GetAllAsync();

    Task CreateAsync(SubCategoryCreateDto subCategoryCreateDto);

    Task<SubCategoryGetDto> UpdateAsync(int id, SubCategoryUpdateDto subCategoryUpdateDto);

    Task DeleteAsync(int id);

    Task SoftDeleteAsync(int id);
}