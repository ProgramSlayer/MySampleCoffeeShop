using CoffeeShop.Models;
using CoffeeShop.Models.Coffees;
using System.Net.Http.Json;
using System.Text.Json;

namespace CoffeeShop.Web.Client.Services.Coffees;

public class CoffeesService : ICoffeesService
{
    private const string Path = "api/coffees";
    private const string PathWithSlash = "api/coffees/";
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    private readonly HttpClient _httpClient;

    public CoffeesService(HttpClient httpClient) 
        => _httpClient = httpClient;

    public async Task<IEnumerable<CoffeeResponseModel>> GetAllAsync()
    {
        var coffees = await _httpClient
            .GetFromJsonAsync<IEnumerable<CoffeeResponseModel>>(Path, Options);
        return coffees;
    }

    public async Task<CoffeeResponseModel> GetByIdAsync(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<CoffeeResponseModel>(PathWithSlash + id, Options);
        return result;
    }

    public async Task<Result<CoffeeResponseModel>> CreateAsync(CoffeeRequestModel model)
    {
        var response = await _httpClient.PostAsJsonAsync(Path, model);
        var coffee = await response.Content.ReadFromJsonAsync<CoffeeResponseModel>(Options);
        return Result<CoffeeResponseModel>.Created(coffee);
    }

    public async Task<Result> UpdateAsync(int id, CoffeeRequestModel model)
    {
        var response = await _httpClient.PutAsJsonAsync(PathWithSlash + id, model);
        var result = await ToResult(response);
        return result;
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync(PathWithSlash + id);
        var result = await ToResult(response);
        return result;
    }

    private static async Task<Result> ToResult(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errors = await response.Content.ReadFromJsonAsync<string[]>();
            return Result.Failure((int)response.StatusCode, errors);
        }
        return Result.Success((int)response.StatusCode);
    }
}