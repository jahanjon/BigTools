namespace DataAccess;

public static class Constant
{
    public const string SupplierListViewCreate = @"
		CREATE VIEW ""SupplierListView"" AS
		SELECT
			SUPPLIERS.""Id"",
			SUPPLIERS.""Name"",
			SUPPLIERS.""NationalId"",
			SUPPLIERS.""Mobile"",
			USERS.""IsActive""
		FROM
			""Suppliers"" SUPPLIERS
			INNER JOIN ""AspNetUsers"" USERS ON SUPPLIERS.""UserId"" = USERS.""Id"";";

    public const string SupplierListViewDrop = @"
		DROP VIEW ""SupplierListView"";";


    public const string ShopperListViewCreate = @"
		CREATE VIEW ""ShopperListView"" AS
		SELECT
			SHOPPERS.""Id"",
			SHOPPERS.""Name"",
			SHOPPERS.""NationCode"",
			SHOPPERS.""Mobile"",
			USERS.""IsActive""
		FROM
			""Shoppers"" SHOPPERS
			INNER JOIN ""AspNetUsers"" USERS ON SHOPPERS.""UserId"" = USERS.""Id"";";

    public const string ShopperListViewDrop = @"
		DROP VIEW ""ShopperListView"";";

    public const string UserPositionViewCreate = @"
		CREATE OR REPLACE VIEW public.""UserPositionView""
		AS
		SELECT ""Shoppers"".""Id"",
		   ""Shoppers"".""Name"",
		   ""Shoppers"".""UserId"",
		   2 AS ""PositionType""
		FROM ""Shoppers""
		UNION
		SELECT ""Suppliers"".""Id"",
		   ""Suppliers"".""Name"",
		   ""Suppliers"".""UserId"",
		   1 AS ""PositionType""
		FROM ""Suppliers"";";

    public const string UserPositionViewDrop = @"
		DROP VIEW ""UserPositionView"";";
}