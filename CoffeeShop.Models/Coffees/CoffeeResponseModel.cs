using CoffeeShop.Data.Models;

namespace CoffeeShop.Models.Coffees;

public record CoffeeResponseModel(
    int Id,
    string Name,
    decimal UnitSellPriceRoubles,
    int CoffeeSpeciesId,
    string CoffeeSpeciesName,
    string CoffeeSpeciesDisplayName)
{
    public CoffeeResponseModel(Coffee coffee) 
        : this(
        coffee.Id,
        coffee.Name,
        coffee.UnitSellPriceRoubles,
        coffee.CoffeeSpeciesId,
        coffee.CoffeeSpecies.Name,
        coffee.CoffeeSpecies.DisplayName)
    {
    }
}
