using CoffeeShop.Models.Identity;
using CoffeeShop.Models;

namespace CoffeeShop.Web.Client.Services.Authentication;

public interface IAuthenticationService
{
    Task<Result> Login(LoginRequestModel model);
    Task Logout();
    Task<Result> Register(RegisterRequestModel model);
}
