using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Data.Configurations;

public class CoffeeConfiguration : IEntityTypeConfiguration<Coffee>
{
    public void Configure(EntityTypeBuilder<Coffee> coffee)
    {
        coffee
            .HasKey(c => c.Id);

        coffee
            .Property(c => c.Name)
            .HasMaxLength(ModelConstants.Coffee.MaxNameLength)
            .IsRequired();

        coffee
            .Property(c => c.UnitSellPriceRoubles)
            .HasPrecision(18, 2)
            .IsRequired();

        coffee
            .HasOne(c => c.CoffeeSpecies)
            .WithMany(cs => cs.Coffees)
            .HasForeignKey(c => c.CoffeeSpeciesId)
            .HasPrincipalKey(cs => cs.Id)
            .OnDelete(DeleteBehavior.Restrict);

        coffee
            .HasIndex(c => c.IsDeleted);

        coffee
            .HasQueryFilter(c => !c.IsDeleted);
    }
}
