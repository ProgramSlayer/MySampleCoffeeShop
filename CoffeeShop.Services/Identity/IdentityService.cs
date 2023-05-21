using CoffeeShop.Data.Models;
using CoffeeShop.Models;
using CoffeeShop.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace CoffeeShop.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<CoffeeShopUser> _userManager;
    private readonly IJwtGeneratorService _jwtGeneratorService;

    public IdentityService(UserManager<CoffeeShopUser> userManager, IJwtGeneratorService jwtGeneratorService)
    {
        _userManager = userManager;
        _jwtGeneratorService = jwtGeneratorService;
    }

    public async Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Result<LoginResponseModel>.BadRequest("Invalid email or password.");
            }

            var signingCredentials = _jwtGeneratorService.GetSigningCredentials();
            var claims = await _jwtGeneratorService.GetClaimsAsync(user);
            var tokenOptions = _jwtGeneratorService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var refreshToken = _jwtGeneratorService.GenerateRefreshToken();
            var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            await _userManager.UpdateAsync(user);

            var loginResponse = new LoginResponseModel(token, refreshToken);
            return Result<LoginResponseModel>.OK(loginResponse);
        }
        catch (Exception ex)
        {
            return Result<LoginResponseModel>.Failure(
                ResultStatusCodes.ResultStatus500InternalServerError,
                $"[{LoginAsync}] : {ex.Message}");
        }
    }

    public async Task<Result> RegisterAsync(RegisterRequestModel model)
    {
        try
        {
            var user = new CoffeeShopUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(err => err.Description);
                return Result.BadRequest(errors);
            }
            
            await _userManager.AddToRoleAsync(user, CoffeeShop.Common.Constants.CustomerRole);
            return Result.Success(ResultStatusCodes.ResultStatus201Created);
        }
        catch (Exception ex)
        {
            return Result.Failure(
                ResultStatusCodes.ResultStatus500InternalServerError,
                $"[{RegisterAsync}] : {ex.Message}");
        }
    }
}
