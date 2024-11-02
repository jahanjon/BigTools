using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixShopper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopperBrands");

            migrationBuilder.DropTable(
                name: "ShopperOrders");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "ShopArea",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "ShoppingType",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "LicenceImage",
                table: "Shoppers");

            migrationBuilder.AddColumn<Guid>(
                name: "LicenceImage",
                table: "Shoppers",
                type: "uuid",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "DocOrRentImage",
                table: "Shoppers");

            migrationBuilder.AddColumn<Guid>(
                name: "DocOrRentImage",
                table: "Shoppers",
                type: "uuid",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "BannerImage",
                table: "Shoppers");

            migrationBuilder.AddColumn<Guid>(
                name: "BannerImage",
                table: "Shoppers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Area",
                table: "Shoppers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Shoppers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRetail",
                table: "Shoppers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ShopperFriends",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shoppers_BrandId",
                table: "Shoppers",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoppers_Brands_BrandId",
                table: "Shoppers",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoppers_Brands_BrandId",
                table: "Shoppers");

            migrationBuilder.DropIndex(
                name: "IX_Shoppers_BrandId",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "IsRetail",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ShopperFriends");

            migrationBuilder.AlterColumn<string>(
                name: "LicenceImage",
                table: "Shoppers",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocOrRentImage",
                table: "Shoppers",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BannerImage",
                table: "Shoppers",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shoppers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopArea",
                table: "Shoppers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShoppingType",
                table: "Shoppers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShopperBrands",
                columns: table => new
                {
                    ShopperId = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopperBrands", x => new { x.ShopperId, x.BrandId });
                    table.ForeignKey(
                        name: "FK_ShopperBrands_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopperBrands_Shoppers_ShopperId",
                        column: x => x.ShopperId,
                        principalTable: "Shoppers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopperOrders",
                columns: table => new
                {
                    ShopperId = table.Column<int>(type: "integer", nullable: false),
                    GoodId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NumberOfGoods = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopperOrders", x => new { x.ShopperId, x.GoodId });
                    table.ForeignKey(
                        name: "FK_ShopperOrders_Goods_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopperOrders_Shoppers_ShopperId",
                        column: x => x.ShopperId,
                        principalTable: "Shoppers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopperBrands_BrandId",
                table: "ShopperBrands",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopperOrders_GoodId",
                table: "ShopperOrders",
                column: "GoodId");
        }
    }
}
