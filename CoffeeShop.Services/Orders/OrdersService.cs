using CoffeeShop.Data;
using CoffeeShop.Data.Models;
using CoffeeShop.Models;
using CoffeeShop.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services.Orders;

public class OrdersService : BaseService<Order>, IOrdersService
{
    private const string ExceptionMessageTemplate = "[{0}] : {1}";

    public OrdersService(CoffeeShopDbContext data) : base(data)
    {
    }

    public async Task<Result<OrderResponseModel>> CreateAsync(string userId, OrderRequestModel model)
    {
        try
        {
            var order = new Order
            {
                UserId = userId,
                City = model.City,
                PostalCode = model.PostalCode,
                District = model.District,
                Street = model.Street,
                Building = model.Building,
            };

            var shoppingCartItems = await Data.ShoppingCartItems
                .Where(sci => sci.ShoppingCart.UserId == userId)
                .Include(sci => sci.Coffee)
                .ToListAsync();

            if (!shoppingCartItems.Any())
            {
                return Result<OrderResponseModel>.BadRequest("Shopping cart is empty.");
            }

            var orderItems = shoppingCartItems.Select(sci => new OrderItem
            {
                OrderId = order.Id,
                Order = order,
                CoffeeId = sci.CoffeeId,
                UnitSellPriceRoubles = sci.Coffee.UnitSellPriceRoubles,
                Quantity = sci.Quantity,
            }).ToList();

            Data.RemoveRange(shoppingCartItems);
            await Data.AddRangeAsync(orderItems);
            await Data.SaveChangesAsync();

            var response = new OrderResponseModel(order);
            return Result<OrderResponseModel>.OK(response);
        }
        catch (Exception ex)
        {
            return Result<OrderResponseModel>.InternalServerError(
                string.Format(ExceptionMessageTemplate, nameof(CreateAsync), ex.Message));
        }
    }

    public async Task<Result<IEnumerable<OrderResponseModel>>> GetAllAsync()
    {
        try
        {
            var data = await AllAsNoTracking()
                .Select(o => new OrderResponseModel(o))
                .ToListAsync();
            return Result<IEnumerable<OrderResponseModel>>.OK(data);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<OrderResponseModel>>.InternalServerError(
                string.Format(ExceptionMessageTemplate, nameof(GetAllAsync), ex.Message));
        }
    }

    public async Task<Result<IEnumerable<OrderResponseModel>>> GetByUserIdAsync(string userId)
    {
        try
        {
            var data = await AllAsNoTracking()
                .Where(o => o.UserId == userId)
                .Select(o => new OrderResponseModel(o))
                .ToListAsync();
            return Result<IEnumerable<OrderResponseModel>>.OK(data);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<OrderResponseModel>>.InternalServerError
                (string.Format(ExceptionMessageTemplate, nameof(GetByUserIdAsync), ex.Message));
        }
    }

    public async Task<Result<OrderDetailsResponseModel>> GetDetailsByIdAsync(string id)
    {
        try
        {
            var order = await AllAsNoTracking()
                .Where(o => o.Id == id)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Coffee)
                .FirstOrDefaultAsync();

            if (order is null)
            {
                return Result<OrderDetailsResponseModel>.NotFound($"Order (Id: {id}) not found.");
            }

            var model = new OrderDetailsResponseModel(order);
            return Result<OrderDetailsResponseModel>.OK(model);
        }
        catch (Exception ex)
        {
            return Result<OrderDetailsResponseModel>.InternalServerError
                (string.Format(ExceptionMessageTemplate, nameof(GetDetailsByIdAsync), ex.Message));
        }
    }
}