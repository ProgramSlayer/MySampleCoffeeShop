using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

/// <summary>
/// Кофе.
/// </summary>
public class Coffee : BaseDeletableModel
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Цена продажи (руб./шт).
    /// </summary>
    public required decimal UnitSellPriceRoubles { get; set; }

    /// <summary>
    /// Уникальный идентификатор вида зёрен кофе.
    /// </summary>
    public required int CoffeeSpeciesId { get; set; }

    /// <summary>
    /// Вид зёрен кофе.
    /// </summary>
    public CoffeeSpecies CoffeeSpecies { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>();
}
