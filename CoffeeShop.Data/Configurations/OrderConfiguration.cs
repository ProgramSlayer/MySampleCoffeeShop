using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> order)
    {
        order
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);

        order
            .Property(a => a.City)
            .HasMaxLength(ModelConstants.Address.MaxCityLength)
            .IsRequired();

        order
            .Property(a => a.PostalCode)
            .HasMaxLength(ModelConstants.Address.MaxPostalCodeLength)
            .IsRequired();

        order
            .Property(a => a.District)
            .HasMaxLength(ModelConstants.Address.MaxDistrictLength)
            .IsRequired();

        order
            .Property(a => a.Street)
            .HasMaxLength(ModelConstants.Address.MaxStreetLength)
            .IsRequired();

        order
            .Property(a => a.Building)
            .HasMaxLength(ModelConstants.Address.MaxBuildingLength)
            .IsRequired();
    }
}
