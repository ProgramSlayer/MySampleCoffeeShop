using CoffeeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services;

public abstract class BaseService<TEntity> 
    where TEntity : class
{
    protected CoffeeShopDbContext Data { get; }

    protected BaseService(CoffeeShopDbContext data)
    {
        Data = data;
    }

    protected IQueryable<TEntity> All() => Data.Set<TEntity>();
    protected IQueryable<TEntity> AllAsNoTracking() => Data.Set<TEntity>().AsNoTracking();
}
