using System.Security.Claims;

namespace CoffeeShop.Web.Server.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User
            ?? throw new InvalidOperationException("This request does not have an authenticated user.");
        //var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)
        //    ?? throw new InvalidOperationException("This request does not have user id");
        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        UserId = idClaim?.Value;
    }

    public string? UserId { get; }
}