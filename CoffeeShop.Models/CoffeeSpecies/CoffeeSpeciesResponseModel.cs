namespace CoffeeShop.Models.CoffeeSpecies;

public record CoffeeSpeciesResponseModel(
    int Id,
    string Name,
    string DisplayName)
{
    public CoffeeSpeciesResponseModel(Data.Models.CoffeeSpecies cs) : this(
        cs.Id,
        cs.Name,
        cs.DisplayName)
    {
    }
}
