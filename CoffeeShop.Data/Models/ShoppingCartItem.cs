using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

public class ShoppingCartItem : BaseModel
{
    public required int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; } = null!;
    public required int CoffeeId { get; set; }
    public Coffee Coffee { get; set; } = null!;
    public required int Quantity { get; set; }
}