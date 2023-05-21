namespace CoffeeShop.Data.Contracts;

public interface IInitialData
{
    Type EntityType { get; }
    IEnumerable<object> GetData();
}
