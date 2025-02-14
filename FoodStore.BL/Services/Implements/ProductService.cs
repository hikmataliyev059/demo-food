using AutoMapper;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Helpers.Extensions.File;
using FoodStore.BL.Helpers.Extensions.Slug;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public ProductService(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment env)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _env = env;
    }

    public async Task<ProductGetDto?> GetByIdAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();

        var product = await _productRepository.GetAll(QueryIncludes.Category, QueryIncludes.TagProducts,
                QueryIncludes.ProductImages, QueryIncludes.SubCategory)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) throw new NotFoundException<Product>();

        var productDto = _mapper.Map<ProductGetDto>(product);

        return productDto;
    }

    public async Task<IEnumerable<ProductGetDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAll(QueryIncludes.Category, QueryIncludes.TagProducts,
                QueryIncludes.ProductImages, QueryIncludes.SubCategory)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ProductGetDto>>(products);
    }

    public async Task CreateAsync(ProductCreateDto productCreateDto)
    {
        if (await _productRepository.Table.AnyAsync(p => p.SKU == productCreateDto.SKU))
        {
            throw new AlreadyExistsException<Product>();
        }

        var product = _mapper.Map<Product>(productCreateDto);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ProductUpdateDto productUpdateDto)
    {
        if (id <= 0) throw new NegativeIdException();

        var existingProduct = await _productRepository
            .GetAll(QueryIncludes.Category, QueryIncludes.TagProducts, QueryIncludes.ProductImages,
                QueryIncludes.SubCategory)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingProduct == null) throw new NotFoundException<Product>();

        if (await _productRepository.Table.AnyAsync(p => p.SKU == productUpdateDto.SKU && p.Id != id))
        {
            throw new AlreadyExistsException<Product>();
        }

        existingProduct.TagProducts.Clear();
        existingProduct.ProductImages.Clear();

        _mapper.Map(productUpdateDto, existingProduct);
        existingProduct.UpdatedTime = DateTime.UtcNow;
        existingProduct.Slug = SlugHelper.CreateSlug(existingProduct.Name);
        await _productRepository.Update(existingProduct);
        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();

        var existingProduct = await _productRepository.GetAll()
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingProduct == null) throw new NotFoundException<Product>();

        foreach (var productImage in existingProduct.ProductImages)
        {
            FileUploadExtensions.Delete(_env.WebRootPath, productImage.ImgUrl);
        }

        await _productRepository.Delete(existingProduct);
        await _productRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();

        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null) throw new NotFoundException<Product>();

        await _productRepository.SoftDelete(existingProduct);
        await _productRepository.SaveChangesAsync();
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return false;
        product.Slug = SlugHelper.CreateSlug(product.Name);

        if (product.StockQuantity >= quantity)
        {
            product.StockQuantity -= quantity;
            product.UpdatedTime = DateTime.UtcNow;
            await _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<ProductGetDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _productRepository
            .GetAll(QueryIncludes.Category, QueryIncludes.TagProducts, QueryIncludes.ProductImages,
                QueryIncludes.SubCategory)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductGetDto>>(products);
    }

    public async Task<IEnumerable<ProductGetDto>> GetProductsBySubCategoryAsync(int subcategoryId)
    {
        var products = await _productRepository
            .GetAll(QueryIncludes.Category, QueryIncludes.TagProducts, QueryIncludes.ProductImages,
                QueryIncludes.SubCategory)
            .Where(p => p.SubCategoryId == subcategoryId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductGetDto>>(products);
    }

    public async Task<IEnumerable<ProductGetDto>> GetProductsByTagIdsAsync(IEnumerable<int> tagIds)
    {
        var products = await _productRepository
            .GetAll(QueryIncludes.Category, QueryIncludes.TagProducts, QueryIncludes.ProductImages,
                QueryIncludes.SubCategory)
            .Where(p => p.TagProducts.Any(tp => tagIds.Contains(tp.TagId)))
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductGetDto>>(products);
    }
}