using Blazored.LocalStorage;
using CoffeeShop.Web.Client.Services.Features;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace CoffeeShop.Web.Client.Services.AuthProviders;

public class ApiAuthStateProvider : AuthenticationStateProvider
{
    private const string JwtAuthType = "JwtAuthType";
    private readonly AuthenticationState Anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));
    
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ApiAuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");
        
        if (string.IsNullOrWhiteSpace(token))
        {
            return Anonymous;
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(
            new ClaimsPrincipal(
                new ClaimsIdentity(
                    JwtParser.ParseClaimsFromJwt(token),
                    JwtAuthType)));
    }

    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), JwtAuthType));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(Anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}
