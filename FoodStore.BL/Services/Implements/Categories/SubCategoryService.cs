using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Category;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Helpers.Extensions.Slug;
using FoodStore.BL.Services.Interfaces.Categories;
using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Repositories.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements.Categories;

public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public SubCategoryService(ISubCategoryRepository subCategoryRepository, IMapper mapper,
        ICategoryRepository categoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<SubCategoryGetDto?> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new NegativeIdException();
        }

        var subCategory = await _subCategoryRepository.GetWhere(sc => sc.Id == id)
            .Include(sc => sc.Category)
            .FirstOrDefaultAsync();

        return subCategory != null
            ? _mapper.Map<SubCategoryGetDto>(subCategory)
            : throw new NotFoundException<SubCategory>();
    }

    public async Task<IEnumerable<SubCategoryGetDto>> GetAllAsync()
    {
        var subCategories = await _subCategoryRepository.GetAll()
            .Include(sc => sc.Category)
            .ToListAsync();

        return _mapper.Map<IEnumerable<SubCategoryGetDto>>(subCategories);
    }


    public async Task CreateAsync(SubCategoryCreateDto subCategoryCreateDto)
    {
        if (await _subCategoryRepository.Table.AnyAsync(c => c.Name == subCategoryCreateDto.Name))
        {
            throw new AlreadyExistsException<SubCategory>();
        }

        var categoryExists = await _categoryRepository.Table.AnyAsync(c => c.Id == subCategoryCreateDto.CategoryId);
        if (!categoryExists) throw new NotFoundException<Category>();

        var subCategory = _mapper.Map<SubCategory>(subCategoryCreateDto);

        await _subCategoryRepository.AddAsync(subCategory);
        await _subCategoryRepository.SaveChangesAsync();
    }

    public async Task<SubCategoryGetDto> UpdateAsync(int id, SubCategoryUpdateDto subCategoryUpdateDto)
    {
        if (id <= 0)
        {
            throw new NegativeIdException();
        }

        if (await _subCategoryRepository.Table.AnyAsync(c => c.Name == subCategoryUpdateDto.Name && c.Id != id))
        {
            throw new AlreadyExistsException<Category>();
        }

        var existingSubCategory = await _subCategoryRepository.GetByIdAsync(id);
        if (existingSubCategory == null) throw new NotFoundException<SubCategory>();

        _mapper.Map(subCategoryUpdateDto, existingSubCategory);
        existingSubCategory.Slug = SlugHelper.CreateSlug(existingSubCategory.Name);

        await _subCategoryRepository.Update(existingSubCategory);
        await _subCategoryRepository.SaveChangesAsync();
        return _mapper.Map<SubCategoryGetDto>(existingSubCategory);
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new NegativeIdException();
        }

        var existingSubCategory = await _subCategoryRepository.GetByIdAsync(id);
        if (existingSubCategory == null) throw new NotFoundException<SubCategory>();

        await _subCategoryRepository.Delete(existingSubCategory);
        await _subCategoryRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new NegativeIdException();
        }

        var existingSubCategory = await _subCategoryRepository.GetByIdAsync(id);
        if (existingSubCategory == null) throw new NotFoundException<SubCategory>();

        await _subCategoryRepository.SoftDelete(existingSubCategory);
        await _subCategoryRepository.SaveChangesAsync();
    }
}