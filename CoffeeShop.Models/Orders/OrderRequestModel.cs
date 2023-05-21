using System.ComponentModel.DataAnnotations;
using static CoffeeShop.Data.ModelConstants.Address;

namespace CoffeeShop.Models.Orders;

public class OrderRequestModel
{
    [Required]
    [MaxLength(MaxCityLength)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(MaxPostalCodeLength)]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(MaxDistrictLength)]
    public string District { get; set; } = string.Empty;

    [Required]
    [MaxLength(MaxStreetLength)]
    public string Street { get; set; } = string.Empty;

    [Required]
    [MaxLength(MaxBuildingLength)]
    public string Building { get; set; } = string.Empty;
}
