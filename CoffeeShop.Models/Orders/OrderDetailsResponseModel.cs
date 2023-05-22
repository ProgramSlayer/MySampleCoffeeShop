using CoffeeShop.Data.Models;
using System.Globalization;

namespace CoffeeShop.Models.Orders;

public class OrderDetailsResponseModel
{
    // For JSON serialization.
    public OrderDetailsResponseModel()
        : this(default, default, default, default, default, default)
    {
    }

    public OrderDetailsResponseModel(
        string id,
        string userId,
        string createdOn,
        string? modifiedOn,
        OrderAddressResponseModel address,
        IEnumerable<OrderItemResponseModel> items)
    {
        Id = id;
        UserId = userId;
        CreatedOn = createdOn;
        ModifiedOn = modifiedOn;
        Address = address;
        Items = items;
    }

    public OrderDetailsResponseModel(Order order)
        : this(
            order.Id,
            order.UserId,
            order.CreatedOn.ToString(CultureInfo.InvariantCulture),
            order.ModifiedOn?.ToString(CultureInfo.InvariantCulture),
            new OrderAddressResponseModel(order),
            OrderItemResponseModel.FromOrderItems(order.Items))
    {
    }

    public string Id { get; set; }
    public string UserId { get; set; }
    public string CreatedOn { get; set; }
    public string? ModifiedOn { get; set; }
    public OrderAddressResponseModel Address { get; set; }
    public IEnumerable<OrderItemResponseModel> Items { get; set; }
}
