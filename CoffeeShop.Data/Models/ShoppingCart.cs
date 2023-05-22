using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

/// <summary>
/// Корзина товаров.
/// </summary>
public class ShoppingCart : BaseModel
{
    /// <summary>
    /// Уникальный идентификатор корзины.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Пользователь.
    /// </summary>
    public CoffeeShopUser User { get; set; } = null!;

    /// <summary>
    /// Товары в корзине.
    /// </summary>
    public ICollection<ShoppingCartItem> Items { get; set; } = new HashSet<ShoppingCartItem>();
}
