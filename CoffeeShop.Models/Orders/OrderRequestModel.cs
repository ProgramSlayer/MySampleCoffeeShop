using System.ComponentModel.DataAnnotations;
using static CoffeeShop.Data.ModelConstants.Address;

namespace CoffeeShop.Models.Orders;

public class OrderRequestModel
{
    [Display(Name = "Название города")]
    [Required(ErrorMessage = "Укажите {0}.")]
    [MaxLength(MaxCityLength, ErrorMessage = "{0} не должно быть длиннее {1} символов.")]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Почтовый индекс")]
    [Required(ErrorMessage = "Укажите {0}.")]
    [MaxLength(MaxPostalCodeLength, ErrorMessage = "{0} не должен быть длиннее {1} символов.")]
    public string PostalCode { get; set; } = string.Empty;

    [Display(Name = "Название района")]
    [Required(ErrorMessage = "Укажите {0}.")]
    [MaxLength(MaxCityLength, ErrorMessage = "{0} не должно быть длиннее {1} символов.")]
    public string District { get; set; } = string.Empty;

    [Display(Name = "Название улицы")]
    [Required(ErrorMessage = "Укажите {0}.")]
    [MaxLength(MaxCityLength, ErrorMessage = "{0} не должно быть длиннее {1} символов.")]
    public string Street { get; set; } = string.Empty;

    [Display(Name = "Номер здания")]
    [Required(ErrorMessage = "Укажите {0}.")]
    [MaxLength(MaxCityLength, ErrorMessage = "{0} не должен быть длиннее {1} символов.")]
    public string Building { get; set; } = string.Empty;
}
