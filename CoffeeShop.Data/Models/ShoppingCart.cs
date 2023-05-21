using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

public class ShoppingCart : BaseModel
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public CoffeeShopUser User { get; set; } = null!;
    public ICollection<ShoppingCartItem> Items { get; set; } = new HashSet<ShoppingCartItem>();
}
