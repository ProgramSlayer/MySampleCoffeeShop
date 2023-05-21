using CoffeeShop.Data.Contracts;
using CoffeeShop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static CoffeeShop.Common.Constants;

namespace CoffeeShop.Data;

public class CoffeeShopDbInitializer : IInitializer
{
    private readonly CoffeeShopDbContext _db;
    private readonly UserManager<CoffeeShopUser> _userManager;
    private readonly RoleManager<CoffeeShopRole> _roleManager;
    private readonly IEnumerable<IInitialData> _initialDataProviders;

    public CoffeeShopDbInitializer(
        CoffeeShopDbContext db,
        UserManager<CoffeeShopUser> userManager,
        RoleManager<CoffeeShopRole> roleManager,
        IEnumerable<IInitialData> initialDataProviders)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _initialDataProviders = initialDataProviders;
    }

    public void Initialize()
    {
        _db.Database.Migrate();
        AddRole(AdminRole);
        AddRole(CustomerRole);
        AddUser(email: "admin@coffeeshop.com", password: "admin123456", roleName: AdminRole);
        AddUser(email: "customer@coffeeshop.com", password: "customer123456", roleName: CustomerRole);
        foreach (var initDataProvider in _initialDataProviders)
        {
            if (DataSetIsEmpty(initDataProvider.EntityType))
            {
                var data = initDataProvider.GetData();

                foreach (var entity in data)
                {
                    _db.Add(entity);
                }
            }
        }
        _db.SaveChanges();
    }

    private void AddUser(string email, string password, string roleName)
    {
        Task.Run(async () =>
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                return;
            }

            user = new CoffeeShopUser
            {
                UserName = email,
                Email = email,
            };

            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, roleName);
        })
        .GetAwaiter()
        .GetResult();
    }

    private void AddRole(string roleName)
    {
        Task.Run(async () =>
        {
            var existingRole = await _roleManager.FindByNameAsync(roleName);
            if (existingRole is not null)
            {
                return;
            }
            existingRole = new CoffeeShopRole(roleName); 
            await _roleManager.CreateAsync(existingRole);
        })
        .GetAwaiter()
        .GetResult();
    }

    private bool DataSetIsEmpty(Type type)
    {
        var setMethod = GetType()
            .GetMethod(nameof(GetSet), BindingFlags.Instance | BindingFlags.NonPublic)
            ?.MakeGenericMethod(type);

        var set = setMethod?.Invoke(this, Array.Empty<object>());

        var countMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == nameof(Queryable.Count) && m.GetParameters().Length == 1)
            .MakeGenericMethod(type);

        var result = (int)countMethod.Invoke(null, new[] { set })!;

        return result == 0;
    }

    private DbSet<TEntity> GetSet<TEntity>()
        where TEntity : class
    {
        return _db.Set<TEntity>();
    }
}
