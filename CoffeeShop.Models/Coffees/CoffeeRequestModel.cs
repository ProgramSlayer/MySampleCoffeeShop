using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models.Coffees;

public class CoffeeRequestModel
{
    [Required]
    [StringLength(
        Data.ModelConstants.Coffee.MaxNameLength,
        ErrorMessage = ErrorMessages.StringLengthErrorMessage,
        MinimumLength = Data.ModelConstants.Common.MinNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(
        typeof(decimal),
        Data.ModelConstants.Coffee.MinUnitSellPriceRoubles,
        Data.ModelConstants.Coffee.MaxUnitSellPriceRoubles)]
    public decimal UnitSellPriceRoubles { get; set; }

    [Required]
    public int CoffeeSpeciesId { get; set; }
}
