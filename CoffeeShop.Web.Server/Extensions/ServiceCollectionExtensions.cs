using CoffeeShop.Data;
using CoffeeShop.Data.Contracts;
using CoffeeShop.Data.Models;
using CoffeeShop.Data.Seed;
using CoffeeShop.Models;
using CoffeeShop.Services.Coffees;
using CoffeeShop.Services.CoffeeSpecies;
using CoffeeShop.Services.Identity;
using CoffeeShop.Services.Orders;
using CoffeeShop.Services.ShoppingCarts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoffeeShop.Web.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCors(this IServiceCollection services, string policyName = "CorsPolicy")
    {
        services.AddCors(p =>
        {
            p.AddPolicy(policyName, o =>
            {
                o
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext<CoffeeShopDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddTransient<IInitialData, CoffeeSpeciesData>()
            .AddTransient<IInitialData, CoffeesData>()
            .AddTransient<IInitializer, CoffeeShopDbInitializer>();

        return services;
    }

    public static IServiceCollection AddIdentity(
        this IServiceCollection services)
    {
        services
            .AddIdentity<CoffeeShopUser, CoffeeShopRole>(o =>
            {
                o.Password.RequiredLength = ModelConstants.Identity.MinPasswordLength;
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<CoffeeShopDbContext>();

        return services;
    }

    public static JwtSettings GetJwtSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = configuration.GetRequiredSection(nameof(JwtSettings));
        services.Configure<JwtSettings>(config);
        var settings = config.Get<JwtSettings>();
        return settings!;
    }

    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        JwtSettings jwtSettings)
    {
        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
                };
            });

        return services;
    }

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services
            .AddScoped<ICoffeesService, CoffeesService>()
            .AddScoped<ICoffeeSpeciesService, CoffeeSpeciesService>()
            .AddScoped<IJwtGeneratorService, JwtGeneratorService>()
            .AddScoped<IIdentityService, IdentityService>()
            .AddScoped<IOrdersService, OrdersService>()
            .AddScoped<IShoppingCartsService, ShoppingCartsService>();
        
        return services;
    }
}
