using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models.Identity;

public class RegisterRequestModel : LoginRequestModel
{
    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Укажите {0}.")]
    [MinLength(Data.ModelConstants.Identity.MinPasswordLength, 
        ErrorMessage = "{0} должен содержать как минимум {1} символов.")]
    [Compare(nameof(Password), ErrorMessage = ErrorMessages.PasswordsDoNotMatchErrorMessage)]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;
}
