using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class shopperEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoppers_Brands_BrandId",
                table: "Shoppers");

            migrationBuilder.DropIndex(
                name: "IX_Shoppers_BrandId",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "BannerImage",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "DocOrRentImage",
                table: "Shoppers");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Shoppers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "Shoppers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HomeCityId",
                table: "Shoppers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HomePostalCode",
                table: "Shoppers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LicenseCode",
                table: "Shoppers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                table: "Shoppers",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_ShopperBrands_BrandId",
                table: "ShopperBrands",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopperBrands");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "HomeCityId",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "HomePostalCode",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "LicenseCode",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "PersonName",
                table: "Shoppers");

            migrationBuilder.AddColumn<Guid>(
                name: "BannerImage",
                table: "Shoppers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Shoppers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocOrRentImage",
                table: "Shoppers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shoppers_BrandId",
                table: "Shoppers",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoppers_Brands_BrandId",
                table: "Shoppers",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }
    }
}
