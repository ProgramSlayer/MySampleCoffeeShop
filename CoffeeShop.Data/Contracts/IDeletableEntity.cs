namespace CoffeeShop.Data.Contracts;

/// <summary>
/// Soft-deletable entity.
/// </summary>
public interface IDeletableEntity
{
    bool IsDeleted { get; set; }
    DateTime? DeletedOn { get; set; }
}
