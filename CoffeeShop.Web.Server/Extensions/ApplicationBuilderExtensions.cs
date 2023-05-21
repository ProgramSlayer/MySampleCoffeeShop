using CoffeeShop.Data.Contracts;

namespace CoffeeShop.Web.Server.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder Initialize(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var initializers = serviceScope.ServiceProvider.GetServices<IInitializer>();
        foreach ( var initializer in initializers )
        {
            initializer.Initialize();
        }

        return app;
    }
}
