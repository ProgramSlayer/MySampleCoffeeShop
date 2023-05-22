using CoffeeShop.Models.CoffeeSpecies;

namespace CoffeeShop.Web.Client.Services.CoffeeSpecies;

public interface ICoffeeSpeciesService
{
    Task<IEnumerable<CoffeeSpeciesResponseModel>> GetAllAsync();
}
