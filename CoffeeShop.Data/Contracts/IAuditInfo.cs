namespace CoffeeShop.Data.Contracts;

/// <summary>
/// Entity that supports audit.
/// </summary>
public interface IAuditInfo
{
    DateTime CreatedOn { get; set; }
    DateTime? ModifiedOn { get; set; }
}
