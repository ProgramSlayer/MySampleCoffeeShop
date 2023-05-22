using Blazored.LocalStorage;
using System.Net.Http.Headers;
using Toolbelt.Blazor;

namespace CoffeeShop.Web.Client.Services;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly ILocalStorageService _localStorage;

    public HttpInterceptorService(HttpClientInterceptor interceptor, ILocalStorageService localStorage)
    {
        _interceptor = interceptor;
        _localStorage = localStorage;
    }

    public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

    public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        if (e.Request.Headers.Authorization?.Scheme != "bearer")
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }

    public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
}