using AutoMapper;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Helpers.Exceptions.User;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Entities.User;
using FoodStore.Core.Entities.Wishlists;
using FoodStore.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public WishlistService(IWishlistRepository wishlistRepository, IProductRepository productRepository,
        UserManager<AppUser> userManager, IMapper mapper)
    {
        _wishlistRepository = wishlistRepository;
        _productRepository = productRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task AddToWishlistAsync(int productId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new UserLoginFaildException("You must be login to add items to your wishlist");

        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) throw new NotFoundException<Product>();

        if (!await _wishlistRepository.Table.AnyAsync(w => w.UserId == userId && w.ProductId == productId))
        {
            var wishlist = new Wishlist
            {
                UserId = userId,
                ProductId = productId
            };

            await _wishlistRepository.AddAsync(wishlist);
            await _wishlistRepository.SaveChangesAsync();
        }
    }

    public async Task RemoveFromWishlistAsync(int productId, string userId)
    {
        var wishlistItem = await _wishlistRepository.GetWhere(w => w.UserId == userId && w.ProductId == productId)
            .FirstOrDefaultAsync();
        if (wishlistItem == null) throw new NotFoundException<Wishlist>();

        await _wishlistRepository.Delete(wishlistItem);
        await _wishlistRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductGetDto>> GetWishlistAsync(string userId)
    {
        var product = await _productRepository
            .GetAll(QueryIncludes.Category, QueryIncludes.SubCategory, QueryIncludes.ProductImages,
                QueryIncludes.TagProducts).ToListAsync();

        if (product == null) throw new NotFoundException<Product>();

        var wishlist = await _wishlistRepository.GetWhere(w => w.UserId == userId)
            .Include(w => w.Product)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductGetDto>>(wishlist.Select(w => w.Product));
    }
}