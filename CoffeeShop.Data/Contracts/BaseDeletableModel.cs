namespace CoffeeShop.Data.Contracts;

public abstract class BaseDeletableModel : BaseModel, IDeletableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
}
