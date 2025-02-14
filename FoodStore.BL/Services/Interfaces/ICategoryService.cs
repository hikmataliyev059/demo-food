using FoodStore.BL.Helpers.DTOs.Category;

namespace FoodStore.BL.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryGetDto?> GetByIdAsync(int id);

    Task<IEnumerable<CategoryGetDto>> GetAllAsync();

    Task CreateAsync(CategoryCreateDto categoryCreateDto);

    Task UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto);

    Task DeleteAsync(int id);

    Task SoftDeleteAsync(int id);

    Task<CategoryWithSubcategoriesDto> GetCategoryByIdAsync(int id);

    Task<IEnumerable<CategoryWithSubcategoriesDto>> GetCategoriesWithSubcategoriesAsync();
}