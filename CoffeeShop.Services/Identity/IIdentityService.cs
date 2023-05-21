using CoffeeShop.Models;
using CoffeeShop.Models.Identity;
using CoffeeShop.Services.Common;

namespace CoffeeShop.Services.Identity;

public interface IIdentityService : IService
{
    public Task<Result> RegisterAsync(RegisterRequestModel model);
    public Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model);
}
