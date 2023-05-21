using CoffeeShop.Models;
using CoffeeShop.Models.Identity;
using CoffeeShop.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identity;

    public IdentityController(IIdentityService identity)
    {
        _identity = identity;
    }

    [HttpPost(nameof(Register))]
    public async Task<ActionResult<Result>> Register([FromBody] RegisterRequestModel model)
    {
        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _identity.RegisterAsync(model);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(nameof(Login))]
    public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel model)
    {
        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _identity.LoginAsync(model);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
