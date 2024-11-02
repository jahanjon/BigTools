using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class SupplierSomeFieldNamesEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstallmentsMonth",
                table: "Suppliers",
                newName: "InstallmentsDays");

            migrationBuilder.RenameColumn(
                name: "HasSpreader",
                table: "Suppliers",
                newName: "HasSpread");

            migrationBuilder.RenameColumn(
                name: "CashMonth",
                table: "Suppliers",
                newName: "CashDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstallmentsDays",
                table: "Suppliers",
                newName: "InstallmentsMonth");

            migrationBuilder.RenameColumn(
                name: "HasSpread",
                table: "Suppliers",
                newName: "HasSpreader");

            migrationBuilder.RenameColumn(
                name: "CashDays",
                table: "Suppliers",
                newName: "CashMonth");
        }
    }
}
