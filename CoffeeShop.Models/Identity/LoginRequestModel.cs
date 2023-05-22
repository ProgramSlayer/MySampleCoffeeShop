using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models.Identity;

public class LoginRequestModel
{
    [Display(Name = "Адрес электронной почты")]
    [Required(ErrorMessage = "Укажите {0}.")]
    [EmailAddress(ErrorMessage = "Укажите правильный адрес электронной почты.")]
    [MinLength(Data.ModelConstants.Identity.MinEmailLength,
        ErrorMessage = "{0} должен содержать как минимум {1} символов.")]
    [MaxLength(Data.ModelConstants.Identity.MaxEmailLength,
        ErrorMessage = "{0} не должен быть длиннее {1} символов.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Укажите {0}.")]
    [MinLength(Data.ModelConstants.Identity.MinPasswordLength,
        ErrorMessage = "{0} должен содержать как минимум {1} символов.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
