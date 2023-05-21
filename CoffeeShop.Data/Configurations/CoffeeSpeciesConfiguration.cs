using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Data.Configurations;

public class CoffeeSpeciesConfiguration : IEntityTypeConfiguration<CoffeeSpecies>
{
    public void Configure(EntityTypeBuilder<CoffeeSpecies> coffeeSpecies)
    {
        coffeeSpecies
            .Property(cs => cs.Name)
            .HasMaxLength(ModelConstants.Common.MaxNameLength)
            .IsRequired();
        
        coffeeSpecies
            .Property(cs => cs.DisplayName)
            .HasMaxLength(ModelConstants.Common.MaxNameLength)
            .IsRequired();
        
        coffeeSpecies
            .HasIndex(cs => cs.IsDeleted);
        
        coffeeSpecies
            .HasQueryFilter(cs  => !cs.IsDeleted);
    }
}
