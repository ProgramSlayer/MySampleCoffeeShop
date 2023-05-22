using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

/// <summary>
/// Заказ.
/// </summary>
public class Order : BaseModel
{
    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Уникальный идентификатор заказчика.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Заказчик.
    /// </summary>
    public CoffeeShopUser User { get; set; } = null!;

    /// <summary>
    /// Город доставки.
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Почтовый индекс.
    /// </summary>
    public required string PostalCode { get; set; }

    /// <summary>
    /// Район.
    /// </summary>
    public required string District { get; set; }

    /// <summary>
    /// Улица.
    /// </summary>
    public required string Street { get; set; }

    /// <summary>
    /// Здание.
    /// </summary>
    public required string Building { get; set; }

    /// <summary>
    /// Товарные позиции заказа.
    /// </summary>
    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
}
