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
    public async Task<ActionResult> Register([FromBody] RegisterRequestModel model)
    {
        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _identity.RegisterAsync(model);

        if (!result.IsSuccessful)
        {
            return StatusCode(result.StatusCode, result.Errors);
        }

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost(nameof(Login))]
    public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel model)
    {
        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _identity.LoginAsync(model);

        if (!result.IsSuccessful)
        {
            return StatusCode(result.StatusCode, result.Errors);
        }

        return Ok(result.Data);
    }
}
