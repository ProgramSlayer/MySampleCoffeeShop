using CoffeeShop.Models;
using CoffeeShop.Models.Orders;

namespace CoffeeShop.Web.Client.Services.Orders;

public interface IOrdersService
{
    Task<IEnumerable<OrderResponseModel>> GetAllAsync();
    Task<OrderDetailsResponseModel> GetDetailsByIdAsync(string id);
    Task<IEnumerable<OrderResponseModel>> GetMyOrdersAsync();
    Task<Result<OrderResponseModel>> MakeAnOrderAsync(OrderRequestModel model);
}
