using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models.ShoppingCarts;

public class ShoppingCartRequestModel
{
    [Display(Name = "Идентификатор кофе")]
    [Required(ErrorMessage = "Укажите {0}.")]
    public int CoffeeId { get; set; }

    [Display(Name = "Количество")]
    [Range(
        Data.ModelConstants.ShoppingCart.MinUnitQuantity, 
        Data.ModelConstants.ShoppingCart.MaxUnitQuantity,
        ErrorMessage = "{0} должно быть не меньше {1} и не больше {2}.")]
    public int Quantity { get; set; }
}
