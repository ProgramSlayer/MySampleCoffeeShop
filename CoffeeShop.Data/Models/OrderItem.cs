using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

/// <summary>
/// Товарная позиция заказа.
/// </summary>
public class OrderItem : BaseModel
{
    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    public required string OrderId { get; set; }

    /// <summary>
    /// Заказ.
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Уникальный идентификатор кофе.
    /// </summary>
    public required int CoffeeId { get; set; }

    /// <summary>
    /// Кофе.
    /// </summary>
    public Coffee Coffee { get; set; } = null!;

    /// <summary>
    /// Цена продажи кофе (руб./шт).
    /// </summary>
    public required decimal UnitSellPriceRoubles { get; set; }

    /// <summary>
    /// Количество кофе (шт).
    /// </summary>
    public required int Quantity { get; set; }
}
