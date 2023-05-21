using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoffeeShop.Data;

public class CoffeeShopDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CoffeeShopDbContext>
{
    public CoffeeShopDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=СoffeeShopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        var builder = new DbContextOptionsBuilder<CoffeeShopDbContext>();
        var options = builder.UseSqlServer(connectionString).Options;
        return new(options);
    }
}
