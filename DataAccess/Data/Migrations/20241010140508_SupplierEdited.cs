using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class SupplierEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cash",
                table: "Suppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CashMonth",
                table: "Suppliers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasImport",
                table: "Suppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasProduce",
                table: "Suppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSpreader",
                table: "Suppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImportDescription",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Installments",
                table: "Suppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentsMonth",
                table: "Suppliers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PreOrder",
                table: "Suppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProduceDescription",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpreaderDescription",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cash",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CashMonth",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "HasImport",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "HasProduce",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "HasSpreader",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ImportDescription",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Installments",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "InstallmentsMonth",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PreOrder",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ProduceDescription",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SpreaderDescription",
                table: "Suppliers");
        }
    }
}
