using CoffeeShop.Data.Models;
using CoffeeShop.Services.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoffeeShop.Services.Identity;

public interface IJwtGeneratorService : IService
{
    public SigningCredentials GetSigningCredentials();
    public Task<List<Claim>> GetClaimsAsync(CoffeeShopUser user);
    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
    public string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
