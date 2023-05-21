using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> orderItem)
    {
        orderItem
            .HasKey(oi => new { oi.OrderId, oi.CoffeeId });

        orderItem
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.Restrict);

        orderItem
            .HasOne(oi => oi.Coffee)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(oi => oi.CoffeeId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Restrict);

        orderItem
            .Property(oi => oi.UnitSellPriceRoubles)
            .HasPrecision(18, 2);
    }
}
