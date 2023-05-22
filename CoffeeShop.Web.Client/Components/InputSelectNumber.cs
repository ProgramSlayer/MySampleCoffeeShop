using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;

namespace CoffeeShop.Web.Client.Components;

public class InputSelectNumber<TValue> : InputSelect<TValue>
{
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (typeof(TValue) != typeof(int))
        {
            return base.TryParseValueFromString(value, out result, out validationErrorMessage);
        }

        if (int.TryParse(value, out var intResult))
        {
            result = (TValue)(object)intResult;
            validationErrorMessage = null;
            return true;
        }

        result = default;
        validationErrorMessage = "The chosen value is not a valid number";
        return false;
    }
}
