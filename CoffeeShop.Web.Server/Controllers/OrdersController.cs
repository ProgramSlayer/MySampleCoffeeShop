using CoffeeShop.Models;
using CoffeeShop.Models.Orders;
using CoffeeShop.Services.Orders;
using CoffeeShop.Web.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _orders;
    private readonly ICurrentUserService _currentUser;

    public OrdersController(IOrdersService orders, ICurrentUserService currentUser)
    {
        _orders = orders;
        _currentUser = currentUser;
    }

    [Authorize(Roles = Common.Constants.AdminRole)]
    [HttpGet("Admin")]
    public async Task<ActionResult<IEnumerable<OrderResponseModel>>> GetAllOrders()
    {
        var result = await _orders.GetAllAsync();

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return Ok(result.Data);
    }

    [HttpGet("{id}", Name = nameof(GetOrderDetailsById))]
    public async Task<ActionResult<OrderDetailsResponseModel>> GetOrderDetailsById(string id)
    {
        var result = await _orders.GetDetailsByIdAsync(id);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return Ok(result.Data);
    }

    [HttpGet($"{nameof(MyOrders)}")]
    public async Task<ActionResult<IEnumerable<OrderResponseModel>>> MyOrders()
    {
        var userId = _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return ToError(Result.InternalServerError("User ID is unknown."));
        }

        var result = await _orders.GetByUserIdAsync(userId);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return Ok(result.Data);
    }

    [HttpPost(Name = nameof(MakeAnOrder))]
    public async Task<ActionResult<OrderResponseModel>> MakeAnOrder(
        [FromBody] OrderRequestModel model)
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

        var result = await _orders.CreateAsync(userId, model);

        if (!result.IsSuccessful)
        {
            return ToError(result);
        }

        return Ok(result.Data);
    }

    private ObjectResult ToError(Result result)
    {
        return StatusCode(
            result.StatusCode,
            result.Errors);
    }
}
