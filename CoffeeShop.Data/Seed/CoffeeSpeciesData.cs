using CoffeeShop.Data.Contracts;
using CoffeeShop.Data.Models;

namespace CoffeeShop.Data.Seed;

public class CoffeeSpeciesData : IInitialData
{
    public Type EntityType => typeof(CoffeeSpecies);

    public IEnumerable<object> GetData()
    {
        return new List<object>()
        {
            new CoffeeSpecies{ Name = "Arabica", DisplayName = "Арабика" },
            new CoffeeSpecies{ Name = "Robusta", DisplayName = "Робуста" },
        };
    }
}
