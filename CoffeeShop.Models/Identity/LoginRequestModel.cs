using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models.Identity;

public class LoginRequestModel
{
    [Required]
    [EmailAddress]
    [MinLength(Data.ModelConstants.Identity.MinEmailLength)]
    [MaxLength(Data.ModelConstants.Identity.MaxEmailLength)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(Data.ModelConstants.Identity.MinPasswordLength)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
