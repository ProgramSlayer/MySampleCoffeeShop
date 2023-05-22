using CoffeeShop.Data.Models;
using System.Globalization;

namespace CoffeeShop.Models.Orders;

public record OrderResponseModel(
    string Id,
    string UserId,
    string CreatedOn,
    string? ModifiedOn)
{
    // For JSON deserialization.
    public OrderResponseModel() : this(default, default, default, default) { }
    public OrderResponseModel(Order order) 
        : this(
        order.Id,
        order.UserId,
        order.CreatedOn.ToString(CultureInfo.InvariantCulture),
        order.ModifiedOn?.ToString(CultureInfo.InvariantCulture))
    {
    }
}
