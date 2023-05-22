using CoffeeShop.Models;
using CoffeeShop.Models.ShoppingCarts;
using CoffeeShop.Services.ShoppingCarts;
using CoffeeShop.Web.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ShoppingCartsController : ControllerBase
{
    private readonly IShoppingCartsService _shoppingCarts;
    private readonly ICurrentUserService _currentUser;

    public ShoppingCartsController(
        IShoppingCartsService shoppingCarts, 
        ICurrentUserService currentUser)
    {
        _shoppingCarts = shoppingCarts;
        _currentUser = currentUser;
    }

    [Authorize(Roles = Common.Constants.AdminRole)]
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<ShoppingCartResponseModel>>> GetAll()
    {
        var result = await _shoppingCarts.GetAllCartsAsync();

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return Ok(result.Data);
    }

    [HttpGet(nameof(GetMyItems))]
    public async Task<ActionResult<IEnumerable<ShoppingCartItemResponseModel>>> GetMyItems()
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return ToError(Result.InternalServerError("User ID is unknown."));
        }

        var result = await _shoppingCarts.GetAllItemsByUserIdAsync(userId);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return Ok(result.Data);
    }

    [HttpPost(nameof(AddItem))]
    public async Task<IActionResult> AddItem([FromBody] ShoppingCartRequestModel model)
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return ToError(Result.InternalServerError("User ID is unknown."));
        }

        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _shoppingCarts.AddItemAsync(model, userId);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return NoContent();
    }

    [HttpPut(nameof(UpdateItem))]
    public async Task<IActionResult> UpdateItem(ShoppingCartRequestModel model)
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return ToError(Result.InternalServerError("User ID is unknown."));
        }

        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _shoppingCarts.UpdateItemAsync(model, userId);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return NoContent();
    }


    [HttpDelete(nameof(RemoveItem) + "/{id}")]
    public async Task<IActionResult> RemoveItem(int id)
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return ToError(Result.InternalServerError("User ID is unknown."));
        }

        var result = await _shoppingCarts.RemoveItemAsync(id, userId);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return NoContent();
    }

    private ObjectResult ToError(Result result)
    {
        return StatusCode(result.StatusCode, result.Errors);
    }
}
