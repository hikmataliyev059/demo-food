using AutoMapper;
using FoodStore.BL.Helpers.Constants;
using FoodStore.BL.Helpers.DTOs.Carts;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Helpers.Exceptions.User;
using FoodStore.BL.Services.Interfaces.Cart;
using FoodStore.Core.Entities.Cart;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Entities.User;
using FoodStore.Core.Repositories.Interfaces.Cart;
using FoodStore.Core.Repositories.Interfaces.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements.Cart;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository,
        UserManager<AppUser> userManager, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task AddToCartAsync(int productId, string userId, int quantity)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new UserLoginFaildException("You must be login to add items to your cart");

        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) throw new NotFoundException<Product>();

        var cartItem = await _cartRepository.GetWhere(c => c.UserId == userId && c.ProductId == productId)
            .FirstOrDefaultAsync();

        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
            await _cartRepository.Update(cartItem);
        }
        else
        {
            cartItem = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity
            };
            await _cartRepository.AddAsync(cartItem);
        }

        await _cartRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<CartGetDto>> GetCartAsync(string userId)
    {
        var product = await _productRepository
            .GetAll(QueryIncludes.Category, QueryIncludes.SubCategory, QueryIncludes.ProductImages,
                QueryIncludes.TagProducts).ToListAsync();
        if (product == null) throw new NotFoundException<Product>();

        var cartItems = await _cartRepository.GetWhere(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        return cartItems.Select(c => new CartGetDto
        {
            Product = _mapper.Map<ProductGetDto>(c.Product),
            Quantity = c.Quantity
        });
    }
    
    public async Task UpdateCartAsync(int productId, string userId, int quantity)
    {
        var cartItem = await _cartRepository.GetWhere(c => c.UserId == userId && c.ProductId == productId)
            .FirstOrDefaultAsync();

        if (cartItem == null)
        {
            throw new NotFoundException<CartItem>();
        }

        cartItem.Quantity = quantity;
        cartItem.UpdatedTime = DateTime.UtcNow.AddHours(4);
        await _cartRepository.Update(cartItem);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int productId, string userId)
    {
        var cartItem = await _cartRepository.GetWhere(c => c.UserId == userId && c.ProductId == productId)
            .FirstOrDefaultAsync();

        if (cartItem == null) throw new NotFoundException<CartItem>();

        await _cartRepository.Delete(cartItem);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task ClearCartAsync(string userId)
    {
        var cartItems = await _cartRepository.GetWhere(c => c.UserId == userId).ToListAsync();

        if (cartItems.Any())
        {
            foreach (var cartItem in cartItems)
            {
                await _cartRepository.Delete(cartItem);
            }

            await _cartRepository.SaveChangesAsync();
        }
    }
}