using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Data.Models;

public class Order : BaseModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string UserId { get; set; }
    public CoffeeShopUser User { get; set; } = null!;
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string District { get; set; }
    public required string Street { get; set; }
    public required string Building { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
}
