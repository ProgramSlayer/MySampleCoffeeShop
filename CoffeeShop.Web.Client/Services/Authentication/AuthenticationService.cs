using Blazored.LocalStorage;
using CoffeeShop.Models;
using CoffeeShop.Models.Identity;
using CoffeeShop.Web.Client.Services.AuthProviders;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace CoffeeShop.Web.Client.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private const string LoginPath = "api/identity/login";
    private const string RegisterPath = "api/identity/register";

    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(
        HttpClient httpClient,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<Result> Login(LoginRequestModel model)
    {
        var response = await _httpClient.PostAsJsonAsync(LoginPath, model);

        if (!response.IsSuccessStatusCode)
        {
            var errors = await response.Content.ReadFromJsonAsync<string[]>();
            return Result.BadRequest(errors);
        }

        var responseStr = await response.Content.ReadAsStringAsync();
        var loginResult = JsonSerializer.Deserialize<LoginResponseModel>(
            responseStr,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var token = loginResult.Token;
        var refreshToken = loginResult.RefreshToken;

        await _localStorage.SetItemAsStringAsync("authToken", token);
        await _localStorage.SetItemAsStringAsync("resfreshToken", refreshToken);

        ((ApiAuthStateProvider)_authenticationStateProvider)?.NotifyUserAuthentication(token);
        _httpClient.DefaultRequestHeaders.Authorization = new("bearer", token);
        return Result.OK;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("refreshToken");
        ((ApiAuthStateProvider)_authenticationStateProvider)?.NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<Result> Register(RegisterRequestModel model)
    {
        var response = await _httpClient.PostAsJsonAsync(RegisterPath, model);

        if (!response.IsSuccessStatusCode)
        {
            var errors = await response.Content.ReadFromJsonAsync<string[]>();
            return Result.BadRequest(errors);
        }

        return Result.Success(201);
    }
}
