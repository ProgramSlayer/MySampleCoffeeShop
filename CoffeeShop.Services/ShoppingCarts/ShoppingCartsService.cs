using CoffeeShop.Data;
using CoffeeShop.Data.Models;
using CoffeeShop.Models;
using CoffeeShop.Models.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services.ShoppingCarts;

public class ShoppingCartsService : BaseService<ShoppingCart>, IShoppingCartsService
{
    private const string ExceptionMessageTemplate = "[{0}] : {1}";

    public ShoppingCartsService(CoffeeShopDbContext data) : base(data)
    {
    }

    public async Task<Result> AddItemAsync(ShoppingCartRequestModel model, string userId)
    {
        try
        {
            var item = await Data
                .ShoppingCartItems
                .Where(i => i.ShoppingCart.UserId == userId && i.CoffeeId == model.CoffeeId)
                .FirstOrDefaultAsync();

            if (item is null)
            {
                item = new()
                {
                    CoffeeId = model.CoffeeId,
                    Quantity = model.Quantity,
                    ShoppingCartId = 0,
                    ShoppingCart = new()
                    {
                        UserId = userId
                    },
                };

                await Data.ShoppingCartItems.AddAsync(item);
            }
            else
            {
                item.Quantity += model.Quantity;
            }

            await Data.SaveChangesAsync();
            return Result.NoContent;
        }
        catch (Exception ex)
        {
            return Result.InternalServerError(
                string.Format(ExceptionMessageTemplate, nameof(AddItemAsync), ex.Message));
        }
    }

    public async Task<Result> RemoveItemAsync(int coffeeId, string userId)
    {
        try
        {
            var item = await Data.ShoppingCartItems
                .Include(i => i.ShoppingCart)
                .Where(i => i.CoffeeId == coffeeId && i.ShoppingCart.UserId == userId)
                .FirstOrDefaultAsync();

            if (item is null)
            {
                return Result.NotFound();
            }

            Data.Remove(item);
            await Data.SaveChangesAsync();
            return Result.NoContent;
        }
        catch (Exception ex)
        {
            return Result.InternalServerError(
                string.Format(ExceptionMessageTemplate, nameof(GetAllCartsAsync), ex.Message));
        }
    }

    public async Task<Result<IEnumerable<ShoppingCartResponseModel>>> GetAllCartsAsync()
    {
        try
        {
            var carts = await AllAsNoTracking()
                .Select(sc => new ShoppingCartResponseModel(sc))
                .ToListAsync();
            return Result<IEnumerable<ShoppingCartResponseModel>>.OK(carts);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ShoppingCartResponseModel>>
                .InternalServerError(
                    string.Format(ExceptionMessageTemplate, nameof(GetAllCartsAsync), ex.Message));
        }
    }

    public async Task<Result<IEnumerable<ShoppingCartItemResponseModel>>> GetAllItemsByUserIdAsync(string userId)
    {
        try
        {
            var items = await Data.ShoppingCartItems
                .AsNoTracking()
                .Include(i => i.Coffee)
                .Include(i => i.ShoppingCart)
                .Where(i => i.ShoppingCart.UserId == userId)
                .Select(i => new ShoppingCartItemResponseModel(i))
                .ToListAsync();
            return Result<IEnumerable<ShoppingCartItemResponseModel>>.OK(items);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ShoppingCartItemResponseModel>>
                .InternalServerError(
                string.Format(ExceptionMessageTemplate, nameof(GetAllCartsAsync), ex.Message));
        }
    }

    public async Task<Result<int>> GetItemCountByUserIdAsync(string userId)
    {
        try
        {
            var count = await Data.ShoppingCartItems
                .AsNoTracking()
                .Include(i => i.ShoppingCart)
                .Where(i => i.ShoppingCart.UserId == userId)
                .CountAsync();
            return Result<int>.OK(count);
        }
        catch (Exception ex)
        {
            return Result<int>.InternalServerError(
                string.Format(ExceptionMessageTemplate, nameof(GetItemCountByUserIdAsync), ex.Message));
        }
    }

    public async Task<Result> UpdateItemAsync(ShoppingCartRequestModel model, string userId)
    {
        try
        {
            var item = await Data.ShoppingCartItems
                .Include(i => i.ShoppingCart)
                .Where(i => i.CoffeeId == model.CoffeeId && i.ShoppingCart.UserId == userId)
                .FirstOrDefaultAsync();

            if (item is null)
            {
                return Result.NotFound($"This user cannot edit this shopping cart.");
            }

            item.Quantity = model.Quantity;

            await Data.SaveChangesAsync();
            
            return Result.NoContent;
        }
        catch (Exception ex)
        {
            return Result.InternalServerError(
                string.Format(ExceptionMessageTemplate, nameof(GetItemCountByUserIdAsync), ex.Message));
        }
    }
}