using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Category;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Helpers.Extensions.Slug;
using FoodStore.BL.Services.Interfaces.Categories;
using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Repositories.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryGetDto?> GetByIdAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();
        
        var category = await _categoryRepository.GetByIdAsync(id);
        return category != null ? _mapper.Map<CategoryGetDto>(category) : throw new NotFoundException<Category>();
    }

    public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAll().ToListAsync();
        var mappedCategories = categories.Select(category => _mapper.Map<CategoryGetDto>(category));
        return mappedCategories;
    }

    public async Task CreateAsync(CategoryCreateDto categoryCreateDto)
    {
        if (await _categoryRepository.Table.AnyAsync(c => c.Name == categoryCreateDto.Name))
        {
            throw new AlreadyExistsException<Category>();
        }

        var category = _mapper.Map<Category>(categoryCreateDto);
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto)
    {
        if (id <= 0) throw new NegativeIdException();

        if (await _categoryRepository.Table.AnyAsync(c => c.Name == categoryUpdateDto.Name && c.Id != id))
        {
            throw new AlreadyExistsException<Category>();
        }

        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null) throw new NotFoundException<Category>();

        _mapper.Map(categoryUpdateDto, existingCategory);
        existingCategory.Slug = SlugHelper.CreateSlug(existingCategory.Name);
        await _categoryRepository.Update(existingCategory);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();
        
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null) throw new NotFoundException<Category>();

        await _categoryRepository.Delete(existingCategory);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();
        
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null) throw new NotFoundException<Category>();

        await _categoryRepository.SoftDelete(existingCategory);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task<CategoryWithSubcategoriesDto> GetCategoryByIdAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();
        
        var category = await _categoryRepository.GetAll()
            .Include(c => c.SubCategories)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            throw new NotFoundException<Category>();
        }

        return _mapper.Map<CategoryWithSubcategoriesDto>(category);
    }

    public async Task<IEnumerable<CategoryWithSubcategoriesDto>> GetCategoriesWithSubcategoriesAsync()
    {
        var categories = await _categoryRepository.GetAll()
            .Include(c => c.SubCategories)
            .ToListAsync();

        return categories.Select(category => _mapper.Map<CategoryWithSubcategoriesDto>(category));
    }
}