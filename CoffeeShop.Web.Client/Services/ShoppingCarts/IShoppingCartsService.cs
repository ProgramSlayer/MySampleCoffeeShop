using CoffeeShop.Models;
using CoffeeShop.Models.ShoppingCarts;

namespace CoffeeShop.Web.Client.Services.ShoppingCarts;

public interface IShoppingCartsService
{
    Task<Result> AddItem(ShoppingCartRequestModel model);
    Task<IEnumerable<ShoppingCartResponseModel>> GetAll();
    Task<IEnumerable<ShoppingCartItemResponseModel>> GetMyItems();
    Task<Result> RemoveItem(int id);
    Task<Result> UpdateItem(ShoppingCartRequestModel model);
}
