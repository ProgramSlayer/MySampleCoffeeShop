using CoffeeShop.Data.Contracts;
using CoffeeShop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data;

public class CoffeeShopDbContext : IdentityDbContext<CoffeeShopUser, CoffeeShopRole, string>
{
    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options)
    {
    }

    public DbSet<CoffeeSpecies> CoffeeSpecies { get; set; }
    public DbSet<Coffee> Coffees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override int SaveChanges()
    {
        ApplyAuditInfoRules();
        ApplyDeletableEntityRules();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfoRules();
        ApplyDeletableEntityRules();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInfoRules()
    {
        ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is IAuditInfo &&
                (e.State == EntityState.Added ||
                e.State == EntityState.Modified))
            .ToList()
            .ForEach(entry =>
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            });
    }

    private void ApplyDeletableEntityRules()
    {
        ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is IDeletableEntity &&
                e.State == EntityState.Deleted)
            .ToList()
            .ForEach(entry =>
            {
                var entity = (IDeletableEntity)entry.Entity;
                entity.IsDeleted = true;
                entity.DeletedOn = DateTime.UtcNow;
                entry.State = EntityState.Modified;
            });
    }
}
