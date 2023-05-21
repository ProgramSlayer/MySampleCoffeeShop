using CoffeeShop.Models.CoffeeSpecies;
using CoffeeShop.Services.CoffeeSpecies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Common.Constants.AdminRole)]
public class CoffeeSpeciesController : ControllerBase
{
    private readonly ICoffeeSpeciesService _coffeeSpecies;

    public CoffeeSpeciesController(ICoffeeSpeciesService coffeeSpecies) 
        => _coffeeSpecies = coffeeSpecies;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoffeeSpeciesResponseModel>>> Get()
    {
        var result = await _coffeeSpecies.GetAllAsync();

        if (!result.IsSuccessful)
        {
            return Problem(); 
        }

        return Ok(result.Data);
    }
}
