using Blazored.LocalStorage;
using Blazored.Toast;
using CoffeeShop.Web.Client;
using CoffeeShop.Web.Client.Services;
using CoffeeShop.Web.Client.Services.Authentication;
using CoffeeShop.Web.Client.Services.AuthProviders;
using CoffeeShop.Web.Client.Services.Coffees;
using CoffeeShop.Web.Client.Services.CoffeeSpecies;
using CoffeeShop.Web.Client.Services.ShoppingCarts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped(sp => new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7144/")
    }.EnableIntercept(sp));

builder.Services.AddHttpClientInterceptor();

builder.Services
    .AddAuthorizationCore()
    .AddBlazoredLocalStorage()
    .AddBlazoredToast()
    .AddScoped<ApiAuthStateProvider>()
    .AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>()
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<ICoffeeSpeciesService, CoffeeSpeciesService>()
    .AddScoped<ICoffeesService, CoffeesService>()
    .AddScoped<IShoppingCartsService, ShoppingCartsService>()
    .AddScoped<HttpInterceptorService>();

await builder.Build().RunAsync();
