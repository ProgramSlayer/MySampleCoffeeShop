namespace CoffeeShop.Data;

public class ModelConstants
{
	public class Common
	{
		public const int MinNameLength = 3;
		public const int MaxNameLength = 100;
	}

	public class Identity
	{
		public const int MinEmailLength = 3;
		public const int MaxEmailLength = 100;
		public const int MinPasswordLength = 6;
	}

	public class Address
	{
		public const int MaxCityLength = 100;
		public const int MaxPostalCodeLength = 100;
		public const int MaxDistrictLength = 100;
		public const int MaxStreetLength = 100;
		public const int MaxBuildingLength = 100;
	}

	public class Coffee
	{
		public const int MaxNameLength = 200;
		public const string MinUnitSellPriceRoubles = "1";
		public const string MaxUnitSellPriceRoubles = "1000000";
	}

	public class ShoppingCart
	{
		public const int MinUnitQuantity = 1;
		public const int MaxUnitQuantity = int.MaxValue;
	}
}
