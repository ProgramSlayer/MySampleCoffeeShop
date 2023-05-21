using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models.Identity;

public class RegisterRequestModel : LoginRequestModel
{
    [Required]
    [MinLength(Data.ModelConstants.Identity.MinPasswordLength)]
    [Compare(nameof(Password), ErrorMessage = ErrorMessages.PasswordsDoNotMatchErrorMessage)]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;
}
