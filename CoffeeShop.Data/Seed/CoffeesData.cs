using CoffeeShop.Data.Contracts;
using CoffeeShop.Data.Models;

namespace CoffeeShop.Data.Seed;

public class CoffeesData : IInitialData
{
    public Type EntityType => typeof(Coffee);

    public IEnumerable<object> GetData()
    {
        return new List<object>()
        {
            new Coffee
            {
                Name = "Кофе Капучино Ароматная Фисташка 0,4 л",
                CoffeeSpeciesId = 1,
                UnitSellPriceRoubles = 169m,
            },
            new Coffee
            {
                Name = "Кофе Латте Ароматная Фисташка 0,4 л",
                CoffeeSpeciesId = 2,
                UnitSellPriceRoubles = 169m,
            },
            new Coffee
            {
                Name = "Кофе Латте 0,2 л",
                CoffeeSpeciesId = 1,
                UnitSellPriceRoubles = 69m,
            },
            new Coffee
            {
                Name = "Кофе Американо 0,2 л",
                CoffeeSpeciesId = 2,
                UnitSellPriceRoubles = 59m,
            },
            new Coffee
            {
                Name = "Кофе Капучино 0,4 л",
                CoffeeSpeciesId = 1,
                UnitSellPriceRoubles = 129m,
            }
        };
    }
}
