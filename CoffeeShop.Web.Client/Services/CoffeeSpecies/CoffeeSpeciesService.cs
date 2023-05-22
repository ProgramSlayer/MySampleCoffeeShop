using CoffeeShop.Models.CoffeeSpecies;
using System.Net.Http.Json;
using System.Text.Json;

namespace CoffeeShop.Web.Client.Services.CoffeeSpecies;

public class CoffeeSpeciesService : ICoffeeSpeciesService
{
    private const string Path = "api/coffeespecies";
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    private readonly HttpClient _httpClient;

    public CoffeeSpeciesService(HttpClient httpClient)
        => _httpClient = httpClient;

    public async Task<IEnumerable<CoffeeSpeciesResponseModel>> GetAllAsync()
    {
        var species = await _httpClient
            .GetFromJsonAsync<IEnumerable<CoffeeSpeciesResponseModel>>(Path, Options);
        return species;
    }
}