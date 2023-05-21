using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models.ShoppingCarts;

public class ShoppingCartRequestModel
{
    [Required]
    public int CoffeeId { get; set; }

    [Range(
        Data.ModelConstants.ShoppingCart.MinUnitQuantity, 
        Data.ModelConstants.ShoppingCart.MaxUnitQuantity)]
    public int Quantity { get; set; }
}
