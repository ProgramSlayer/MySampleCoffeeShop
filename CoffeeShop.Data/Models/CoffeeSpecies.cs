using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

/// <summary>
/// Вид зёрен кофе.
/// </summary>
public class CoffeeSpecies : BaseDeletableModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public ICollection<Coffee> Coffees { get; set; } = new HashSet<Coffee>();
}
