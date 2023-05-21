using CoffeeShop.Models;
using CoffeeShop.Models.ShoppingCarts;
using CoffeeShop.Services.Common;

namespace CoffeeShop.Services.ShoppingCarts;

public interface IShoppingCartsService : IService
{
    Task<Result<IEnumerable<ShoppingCartResponseModel>>> GetAllCartsAsync();
    Task<Result<IEnumerable<ShoppingCartItemResponseModel>>> GetAllItemsByUserIdAsync(string userId);
    Task<Result> AddItemAsync(ShoppingCartRequestModel model, string userId);
    Task<Result> UpdateItemAsync(ShoppingCartRequestModel model, string userId);
    Task<Result> RemoveItemAsync(int coffeeId, string userId);
    Task<Result<int>> GetItemCountByUserIdAsync(string userId);
}
