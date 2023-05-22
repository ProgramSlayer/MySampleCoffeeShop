using CoffeeShop.Data.Models;

namespace CoffeeShop.Models.ShoppingCarts;

public record ShoppingCartItemResponseModel(
    int CoffeeId,
    string CoffeeName,
    decimal UnitSellPriceRoubles,
    int Quantity)
{
    // For JSON deserialization.
    public ShoppingCartItemResponseModel() : this(default, default, default, default) { }

    public ShoppingCartItemResponseModel(ShoppingCartItem sci) : this(
        sci.CoffeeId,
        sci.Coffee.Name,
        sci.Coffee.UnitSellPriceRoubles,
        sci.Quantity)
    {
    }

    public static IEnumerable<ShoppingCartItemResponseModel> FromShoppingCartItems(
        IEnumerable<ShoppingCartItem> items)
    {
        var result = items.Select(i => new ShoppingCartItemResponseModel(i));
        return result;
    }
}
