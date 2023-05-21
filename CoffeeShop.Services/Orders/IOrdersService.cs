using CoffeeShop.Models;
using CoffeeShop.Models.Orders;
using CoffeeShop.Services.Common;

namespace CoffeeShop.Services.Orders;

public interface IOrdersService : IService
{
    public Task<Result<IEnumerable<OrderResponseModel>>> GetAllAsync();
    public Task<Result<IEnumerable<OrderResponseModel>>> GetByUserIdAsync(string userId);
    public Task<Result<OrderDetailsResponseModel>> GetDetailsByIdAsync(string id);
    public Task<Result<OrderResponseModel>> CreateAsync(string userId, OrderRequestModel model);
}
