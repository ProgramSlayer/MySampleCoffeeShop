namespace CoffeeShop.Models;

internal class ErrorMessages
{
    public const string StringLengthErrorMessage
            = "The {0} must be at least {2} and at max {1} characters long.";

    public const string CoffeeNameRequiredMessage
        = "Укажите наименование.";

    public const string CoffeeSpeciesIdRequired
        = "Укажите вид зёрен.";

    public const string CoffeeUnitSellPriceRoublesErrorMessage
        = "{0} должна быть не меньше {1} и не больше {2} руб.";

    public const string CoffeeNameLengthErrorMessage
        = "{0} должно быть не короче {2} символов и не длиннее {1} символов.";

    public const string PasswordsDoNotMatchErrorMessage
        = "Пароли не совпадают.";
}
