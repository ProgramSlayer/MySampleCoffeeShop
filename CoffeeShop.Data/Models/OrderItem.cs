using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

public class OrderItem : BaseModel
{
    public required string OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public required int CoffeeId { get; set; }
    public Coffee Coffee { get; set; } = null!;
    public required decimal UnitSellPriceRoubles { get; set; }
    public required int Quantity { get; set; }
}
