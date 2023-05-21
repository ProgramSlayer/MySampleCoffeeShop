﻿namespace CoffeeShop.Data.Contracts;

public abstract class BaseModel : IAuditInfo
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
