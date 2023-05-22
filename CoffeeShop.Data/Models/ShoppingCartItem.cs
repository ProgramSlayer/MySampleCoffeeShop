using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

/// <summary>
/// Товар в корзине.
/// </summary>
public class ShoppingCartItem : BaseModel
{
    /// <summary>
    /// Уникальный идентификатор корзины.
    /// </summary>
    public required int ShoppingCartId { get; set; }

    /// <summary>
    /// Корзина.
    /// </summary>
    public ShoppingCart ShoppingCart { get; set; } = null!;

    /// <summary>
    /// Уникальный идентификатор кофе.
    /// </summary>
    public required int CoffeeId { get; set; }

    /// <summary>
    /// Кофе.
    /// </summary>
    public Coffee Coffee { get; set; } = null!;

    /// <summary>
    /// Количество кофе (шт).
    /// </summary>
    public required int Quantity { get; set; }
}