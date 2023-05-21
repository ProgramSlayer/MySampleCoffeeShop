using CoffeeShop.Data;
using CoffeeShop.Data.Models;
using CoffeeShop.Models;
using CoffeeShop.Models.Coffees;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services.Coffees;

public class CoffeesService : BaseService<Coffee>, ICoffeesService
{
    private const string CoffeeNotFound = "Coffee (Id: {0}) not found.";
    private const string ExceptionTemplate = "[{0}] : {1}";

    public CoffeesService(CoffeeShopDbContext data) : base(data)
    {
    }

    public async Task<Result<CoffeeResponseModel>> CreateAsync(CoffeeRequestModel model)
    {
        try
        {
            var coffee = new Coffee
            {
                Name = model.Name,
                UnitSellPriceRoubles = model.UnitSellPriceRoubles,
                CoffeeSpeciesId = model.CoffeeSpeciesId,
            };

            await Data.Coffees.AddAsync(coffee);
            await Data.SaveChangesAsync();
            await Data.Entry(coffee).Reference(c => c.CoffeeSpecies).LoadAsync();

            var response = new CoffeeResponseModel(coffee);
            return Result<CoffeeResponseModel>.OK(response);
        }
        catch (Exception ex)
        {
            return Result<CoffeeResponseModel>.InternalServerError(
                string.Format(ExceptionTemplate, nameof(CreateAsync), ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var entry = await GetByIdInternalAsync(id);
            
            if (entry is null)
            {
                return Result.NotFound(string.Format(CoffeeNotFound, id));
            }

            Data.Coffees.Remove(entry);
            await Data.SaveChangesAsync();
            return Result.NoContent;
        }
        catch (Exception ex)
        {
            return Result.InternalServerError(
                string.Format(ExceptionTemplate, nameof(DeleteAsync), ex.Message));
        }
    }

    public async Task<Result<IEnumerable<CoffeeResponseModel>>> GetAllAsync()
    {
        try
        {
            var data = await AllAsNoTracking()
                .Include(c => c.CoffeeSpecies)
                .Select(c => new CoffeeResponseModel(c))
                .ToListAsync();
            return Result<IEnumerable<CoffeeResponseModel>>.OK(data);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CoffeeResponseModel>>.InternalServerError(
                string.Format(ExceptionTemplate, nameof(GetByIdAsync), ex.Message));
        }
    }

    public async Task<Result<CoffeeResponseModel>> GetByIdAsync(int id)
    {
        try
        {
            var coffee = await GetByIdInternalAsync(id);
            if (coffee is null)
            {
                return Result<CoffeeResponseModel>.NotFound(string.Format(CoffeeNotFound, id));
            }
            var model = new CoffeeResponseModel(coffee);
            return Result<CoffeeResponseModel>.OK(model);
        }
        catch (Exception ex)
        {
            return Result<CoffeeResponseModel>.InternalServerError(
                string.Format(ExceptionTemplate, nameof(GetByIdAsync), ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int id, CoffeeRequestModel model)
    {
        try
        {
            var entry = await GetByIdInternalAsync(id);
            
            if (entry is null)
            {
                return Result.NotFound(string.Format(CoffeeNotFound, id));
            }
            
            entry.Name = model.Name;
            entry.UnitSellPriceRoubles = model.UnitSellPriceRoubles;
            entry.CoffeeSpeciesId = model.CoffeeSpeciesId;

            await Data.SaveChangesAsync();
            return Result.NoContent;
        }
        catch (Exception ex)
        {
            return Result.InternalServerError(
                string.Format(ExceptionTemplate, nameof(UpdateAsync), ex.Message));
        }
    }

    private async Task<Coffee?> GetByIdInternalAsync(int id)
    {
        var data = await All()
            .Include(c => c.CoffeeSpecies)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        return data;
    }
}