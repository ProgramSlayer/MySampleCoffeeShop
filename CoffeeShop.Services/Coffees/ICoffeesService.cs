using CoffeeShop.Models;
using CoffeeShop.Models.Coffees;
using CoffeeShop.Services.Common;

namespace CoffeeShop.Services.Coffees;

public interface ICoffeesService : IService
{
    public Task<Result<IEnumerable<CoffeeResponseModel>>> GetAllAsync();
    public Task<Result<CoffeeResponseModel>> GetByIdAsync(int id);
    public Task<Result<CoffeeResponseModel>> CreateAsync(CoffeeRequestModel model);
    public Task<Result> UpdateAsync(int id, CoffeeRequestModel model);
    public Task<Result> DeleteAsync(int id);
}
