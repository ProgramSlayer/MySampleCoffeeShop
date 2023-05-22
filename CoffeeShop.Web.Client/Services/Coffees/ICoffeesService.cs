using CoffeeShop.Models;
using CoffeeShop.Models.Coffees;

namespace CoffeeShop.Web.Client.Services.Coffees;

public interface ICoffeesService
{
    Task<IEnumerable<CoffeeResponseModel>> GetAllAsync();
    Task<CoffeeResponseModel> GetByIdAsync(int id);
    Task<Result<CoffeeResponseModel>> CreateAsync(CoffeeRequestModel model);
    Task<Result> UpdateAsync(int id, CoffeeRequestModel model);
    Task<Result> DeleteAsync(int id);
}
