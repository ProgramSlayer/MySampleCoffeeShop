using CoffeeShop.Models;
using CoffeeShop.Models.Orders;
using System.Net.Http.Json;
using System.Text.Json;

namespace CoffeeShop.Web.Client.Services.Orders;

public class OrdersService : IOrdersService
{
    private const string Path = "api/orders";
    private const string PathWithSlash = "api/orders/";
    private const string GetAllOrdersPath = PathWithSlash + "admin";
    private const string GetMyOrdersPath = PathWithSlash + "myorders";

    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
    
    private readonly HttpClient _httpClient;

    public OrdersService(HttpClient httpClient) 
        => _httpClient = httpClient;

    public async Task<IEnumerable<OrderResponseModel>> GetAllAsync()
    {
        var orders = await _httpClient
            .GetFromJsonAsync<IEnumerable<OrderResponseModel>>(
                GetAllOrdersPath, 
                Options);
        return orders;
    }

    public async Task<OrderDetailsResponseModel> GetDetailsByIdAsync(string id)
    {
        var response = await _httpClient
            .GetFromJsonAsync<OrderDetailsResponseModel>(
                PathWithSlash + id,
                Options);
        return response;
    }

    public async Task<IEnumerable<OrderResponseModel>> GetMyOrdersAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<IEnumerable<OrderResponseModel>>(
                GetMyOrdersPath, Options);
        return response;
    }

    public async Task<Result<OrderResponseModel>> MakeAnOrderAsync(
        OrderRequestModel model)
    {
        var response = await _httpClient
            .PostAsJsonAsync(
                PathWithSlash,
                model);
        var result = await ToResult<OrderResponseModel>(response);
        return result;
    }

    private static async Task<Result<TData>> ToResult<TData>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errors = await response.Content.ReadFromJsonAsync<string[]>();
            return Result<TData>.Failure(
                (int)response.StatusCode,
                errors);
        }

        var data = await response
            .Content
            .ReadFromJsonAsync<TData>(Options);

        return Result<TData>.SuccessWith(
            (int)response.StatusCode,
            data);
    }

}
