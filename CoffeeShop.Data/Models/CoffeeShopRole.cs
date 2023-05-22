using CoffeeShop.Data.Contracts;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.Data.Models;

/// <summary>
/// Роль пользователя магазина кофе.
/// </summary>
public class CoffeeShopRole : IdentityRole, IAuditInfo, IDeletableEntity
{
    public CoffeeShopRole() : this(null!) { }
    public CoffeeShopRole(string name) : base(name) { }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
}
