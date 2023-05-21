using CoffeeShop.Models;
using CoffeeShop.Models.CoffeeSpecies;
using CoffeeShop.Services.Common;

namespace CoffeeShop.Services.CoffeeSpecies;

public interface ICoffeeSpeciesService : IService
{
    public Task<Result<IEnumerable<CoffeeSpeciesResponseModel>>> GetAllAsync();
}
