using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Models.CoffeeSpecies;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services.CoffeeSpecies;

public class CoffeeSpeciesService : BaseService<Data.Models.CoffeeSpecies>, ICoffeeSpeciesService
{
    public CoffeeSpeciesService(CoffeeShopDbContext data) : base(data)
    {
    }

    public async Task<Result<IEnumerable<CoffeeSpeciesResponseModel>>> GetAllAsync()
    {
		try
		{
            var data = await AllAsNoTracking()
                .Select(cs => new CoffeeSpeciesResponseModel(cs))
                .ToListAsync();
            return Result<IEnumerable<CoffeeSpeciesResponseModel>>.OK(data);
		}
		catch (Exception ex)
		{
            return Result<IEnumerable<CoffeeSpeciesResponseModel>>.InternalServerError(
                $"[{GetAllAsync}] : {ex.Message}");
		}
    }
}
