using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Data.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> shoppingCart)
    {
        shoppingCart
            .HasKey(sc => sc.Id);

        shoppingCart
            .HasOne(sc => sc.User)
            .WithMany(u => u.ShoppingCarts)
            .HasForeignKey(sc => sc.UserId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
