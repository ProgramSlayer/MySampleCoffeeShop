using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Data.Configurations;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> item)
    {
        item
            .HasKey(i => new { i.ShoppingCartId, i.CoffeeId });

        item
            .HasOne(i => i.ShoppingCart)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.ShoppingCartId)
            .HasPrincipalKey(c => c.Id);

        item
            .HasOne(i => i.Coffee)
            .WithMany(c => c.ShoppingCartItems)
            .HasForeignKey(i => i.CoffeeId)
            .HasPrincipalKey(c => c.Id);
    }
}
