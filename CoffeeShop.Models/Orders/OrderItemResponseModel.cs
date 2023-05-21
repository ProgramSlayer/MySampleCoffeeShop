using CoffeeShop.Data.Models;

namespace CoffeeShop.Models.Orders;

public record OrderItemResponseModel(
    int CoffeeId,
    string CoffeeName,
    decimal UnitSellPriceRoubles,
    int Quantity)
{
    public OrderItemResponseModel(OrderItem oi) 
        : this(
        oi.CoffeeId,
        oi.Coffee.Name,
        oi.UnitSellPriceRoubles,
        oi.Quantity)
    {
    }

    public static IEnumerable<OrderItemResponseModel> FromOrderItems(IEnumerable<OrderItem> items)
    {
        var result = items.Select(oi => new OrderItemResponseModel(oi));
        return result;
    }
}
