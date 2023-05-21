using CoffeeShop.Data.Models;

namespace CoffeeShop.Models.ShoppingCarts;

public record ShoppingCartDetailsResponseModel(
    int Id,
    string UserId,
    IEnumerable<ShoppingCartItemResponseModel> Items)
{
    public ShoppingCartDetailsResponseModel(ShoppingCart sc)
        : this(
              sc.Id,
              sc.UserId,
              ShoppingCartItemResponseModel.FromShoppingCartItems(sc.Items))
    {
    }
}
