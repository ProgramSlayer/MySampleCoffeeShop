using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

public class Coffee : BaseDeletableModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal UnitSellPriceRoubles { get; set; }
    public required int CoffeeSpeciesId { get; set; }
    public CoffeeSpecies CoffeeSpecies { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>();
}
