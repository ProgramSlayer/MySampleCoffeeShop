using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<CoffeeShopUser>
{
    public void Configure(EntityTypeBuilder<CoffeeShopUser> user)
    {
        user.HasIndex(u => u.IsDeleted);
        user.HasQueryFilter(u => !u.IsDeleted);
    }
}
