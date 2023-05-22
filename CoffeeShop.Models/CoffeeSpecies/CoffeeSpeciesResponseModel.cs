namespace CoffeeShop.Models.CoffeeSpecies;

public record CoffeeSpeciesResponseModel(
    int Id,
    string Name,
    string DisplayName)
{
    // For JSON deserialization.
    public CoffeeSpeciesResponseModel() : this(default, default, default) { }

    public CoffeeSpeciesResponseModel(Data.Models.CoffeeSpecies cs) : this(
        cs.Id,
        cs.Name,
        cs.DisplayName)
    {
    }
}
