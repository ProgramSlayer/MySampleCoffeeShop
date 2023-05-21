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
            return Problem();
        }

        return Ok(result.Data);
    }

    [HttpGet(nameof(GetMyItems))]
    public async Task<ActionResult<IEnumerable<ShoppingCartItemResponseModel>>> GetMyItems()
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Problem("User id is null");
        }

        var result = await _shoppingCarts.GetAllItemsByUserIdAsync(userId);

        if (!result.IsSuccessful)
        {
            return Problem();
        }

        return Ok(result.Data);
    }

    [HttpPost(nameof(AddItem))]
    public async Task<IActionResult> AddItem([FromBody] ShoppingCartRequestModel model)
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Problem("User id is null.");
        }

        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _shoppingCarts.AddItemAsync(model, userId);

        if (!result.IsSuccessful)
        {
            return Problem();
        }

        return NoContent();
    }

    [HttpPut(nameof(UpdateItem))]
    public async Task<IActionResult> UpdateItem(ShoppingCartRequestModel model)
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Problem("User id is null.");
        }

        if (model is null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _shoppingCarts.UpdateItemAsync(model, userId);

        if (!result.IsSuccessful)
        {
            return result.StatusCode == ResultStatusCodes.ResultStatus404NotFound
                ? NotFound()
                : Problem();
        }

        return NoContent();
    }


    [HttpDelete(nameof(RemoveItem) + "/{id}")]
    public async Task<IActionResult> RemoveItem(int id)
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Problem("User id is null.");
        }

        var result = await _shoppingCarts.RemoveItemAsync(id, userId);

        if (!result.IsSuccessful)
        {
            return result.StatusCode == ResultStatusCodes.ResultStatus404NotFound
                ? NotFound()
                : Problem();
        }

        return NoContent();
    }
}
