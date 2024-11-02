using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class supplierModelEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mobile3",
                table: "Suppliers",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "Mobile2",
                table: "Suppliers",
                newName: "Phone2");

            migrationBuilder.AddColumn<string>(
                name: "AccountantMobile",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Suppliers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CompanyNationalId",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CoordinatorMobile",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerMobile",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountantMobile",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CompanyNationalId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CoordinatorMobile",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ManagerMobile",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Suppliers",
                newName: "Mobile3");

            migrationBuilder.RenameColumn(
                name: "Phone2",
                table: "Suppliers",
                newName: "Mobile2");
        }
    }
}
