using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models.Coffees;

public class CoffeeRequestModel
{
    [Display(Name = "Наименование")]
    [Required(ErrorMessage = ErrorMessages.CoffeeNameRequiredMessage)]
    [StringLength(
        Data.ModelConstants.Coffee.MaxNameLength,
        ErrorMessage = ErrorMessages.CoffeeNameLengthErrorMessage,
        MinimumLength = Data.ModelConstants.Common.MinNameLength)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Цена")]
    [Required(ErrorMessage = "Укажите цену продажи.")]
    [Range(
        typeof(decimal),
        Data.ModelConstants.Coffee.MinUnitSellPriceRoubles,
        Data.ModelConstants.Coffee.MaxUnitSellPriceRoubles,
        ErrorMessage = ErrorMessages.CoffeeUnitSellPriceRoublesErrorMessage)]
    public decimal UnitSellPriceRoubles { get; set; }

    [Required(ErrorMessage = ErrorMessages.CoffeeSpeciesIdRequired)]
    public int CoffeeSpeciesId { get; set; }
}
