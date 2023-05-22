using CoffeeShop.Models;
using CoffeeShop.Models.ShoppingCarts;
using System.Net.Http.Json;
using System.Text.Json;

namespace CoffeeShop.Web.Client.Services.ShoppingCarts;

public class ShoppingCartsService : IShoppingCartsService
{
    private const string Path = "api/shoppingcarts";
    private const string PathWithSlash = "api/shoppingcarts/";
    private const string GetAllItemsPath = "api/shoppingcarts/getall";
    private const string GetMyItemsPath = "api/shoppingcarts/getmyitems";
    private const string AddItemPath = "api/shoppingcarts/additem";
    private const string UpdateItemPath = "api/shoppingcarts/updateitem";
    private const string RemoveItemPath = "api/shoppingcarts/removeitem/";


    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    private readonly HttpClient _httpClient;

    public ShoppingCartsService(HttpClient httpClient) 
        => _httpClient = httpClient;

    public async Task<Result> AddItem(ShoppingCartRequestModel model)
    {
        var response = await _httpClient.PostAsJsonAsync(AddItemPath, model);
        var result = await ToResult(response);
        return result;
    }

    public async Task<Result> UpdateItem(ShoppingCartRequestModel model)
    {
        var response = await _httpClient.PutAsJsonAsync(UpdateItemPath, model);
        var result = await ToResult(response);
        return result;
    }

    public async Task<Result> RemoveItem(int id)
    {
        var response = await _httpClient.DeleteAsync(RemoveItemPath +  id);
        var result = await ToResult(response);
        return result;
    }

    public async Task<IEnumerable<ShoppingCartResponseModel>> GetAll()
    {
        var carts = await _httpClient.GetFromJsonAsync<IEnumerable<ShoppingCartResponseModel>>(GetAllItemsPath, Options);
        return carts;
    }

    public async Task<IEnumerable<ShoppingCartItemResponseModel>> GetMyItems()
    {
        var items = await _httpClient.GetFromJsonAsync<IEnumerable<ShoppingCartItemResponseModel>>(GetMyItemsPath, Options);
        return items;
    }

    private static async Task<Result> ToResult(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errors = await response.Content.ReadFromJsonAsync<string[]>();
            return Result.Failure(
                (int)response.StatusCode,
                errors);
        }

        return Result.Success((int)response.StatusCode);
    }
}