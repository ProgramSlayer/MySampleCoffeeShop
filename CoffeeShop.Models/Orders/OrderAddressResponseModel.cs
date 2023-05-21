using CoffeeShop.Data.Models;

namespace CoffeeShop.Models.Orders;

public record OrderAddressResponseModel(
    string City,
    string PostalCode,
    string District,
    string Street,
    string Building)
{
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
