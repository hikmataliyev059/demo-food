// using FoodStore.BL.Services.Interfaces;
// using FoodStore.Core.Entities;
// using FoodStore.Core.Repositories.Interfaces;
// using Microsoft.EntityFrameworkCore;
//
// namespace FoodStore.BL.Services.Implements;
//
// public class ProductWishlistService : IProductWishlistService
// {
//     private readonly IProductWishlistRepository _productWishlistRepository;
//
//     public ProductWishlistService(IProductWishlistRepository productWishlistRepository)
//     {
//         _productWishlistRepository = productWishlistRepository;
//     }
//
//     public async Task AddToWishlistAsync(Guid userId, int productId, int wishlistId)
//     {
//         // Wishlistə məhsul əlavə etmək
//         var existingItem = await _productWishlistRepository.GetWhere(pw =>
//             pw.UserId == userId && pw.ProductId == productId && pw.WishlistId == wishlistId).FirstOrDefaultAsync();
//
//         if (existingItem != null)
//             return; // Məhsul artıq wishlistdə varsa, əlavə etməyə ehtiyac yoxdur
//
//         var productWishlist = new ProductWishlist
//         {
//             UserId = userId,
//             ProductId = productId,
//             WishlistId = wishlistId
//         };
//
//         await _productWishlistRepository.AddAsync(productWishlist);
//         await _productWishlistRepository.SaveChangesAsync();
//     }
//
//     public async Task<IEnumerable<ProductWishlist>> GetWishlistItemsAsync(Guid userId)
//     {
//         return await _productWishlistRepository.GetWhere(pw => pw.UserId == userId)
//             .Include(pw => pw.Product)
//             .Include(pw => pw.Wishlist)
//             .ToListAsync();
//     }
//
//     public async Task RemoveFromWishlistAsync(Guid userId, int productId, int wishlistId)
//     {
//         var item = await _productWishlistRepository.GetWhere(pw =>
//             pw.UserId == userId && pw.ProductId == productId && pw.WishlistId == wishlistId).FirstOrDefaultAsync();
//
//         if (item != null)
//         {
//             _productWishlistRepository.Delete(item);
//             await _productWishlistRepository.SaveChangesAsync();
//         }
//     }
// }