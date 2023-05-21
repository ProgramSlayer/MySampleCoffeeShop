﻿using CoffeeShop.Data.Models;
using System.Globalization;

namespace CoffeeShop.Models.ShoppingCarts;

public record ShoppingCartResponseModel(int Id, string UserId, string CreatedOn, string? ModifiedOn)
{
    public ShoppingCartResponseModel(ShoppingCart sc) 
        : this(
              sc.Id,
              sc.UserId,
              sc.CreatedOn.ToString(CultureInfo.InvariantCulture),
              sc.ModifiedOn?.ToString(CultureInfo.InvariantCulture))
    {
    }
}
