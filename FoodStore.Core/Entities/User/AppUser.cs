using FoodStore.Core.Entities.Cart;
using FoodStore.Core.Entities.Wish;
using Microsoft.AspNetCore.Identity;

namespace FoodStore.Core.Entities.User;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ConfirmKey { get; set; }
    public DateTime? ConfirmKeyExpiration { get; set; } = DateTime.UtcNow.AddHours(4);
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; } = DateTime.UtcNow.AddHours(4);
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}