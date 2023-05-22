using CoffeeShop.Data.Models;

namespace CoffeeShop.Models.Orders;

public record OrderAddressResponseModel(
    string City,
    string PostalCode,
    string District,
    string Street,
    string Building)
{
    // For JSON deserialization.
    public OrderAddressResponseModel()
        : this(default, default, default, default, default)
    { }
    public OrderAddressResponseModel(Order order) : 
        this(
        order.City,
        order.PostalCode,
        order.District,
        order.Street,
        order.Building)
    {
    }
}
