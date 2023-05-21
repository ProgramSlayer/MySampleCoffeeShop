using CoffeeShop.Data.Contracts;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.Data.Models;

public class CoffeeShopUser : IdentityUser, IAuditInfo, IDeletableEntity
{
    public CoffeeShopUser() => Id = Guid.NewGuid().ToString();
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new HashSet<ShoppingCart>();
}
