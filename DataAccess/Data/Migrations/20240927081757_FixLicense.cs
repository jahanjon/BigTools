using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixLicense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicenceImage",
                table: "Shoppers",
                newName: "LicenseImage");

            migrationBuilder.RenameColumn(
                name: "HasLicence",
                table: "Shoppers",
                newName: "HasLicense");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicenseImage",
                table: "Shoppers",
                newName: "LicenceImage");

            migrationBuilder.RenameColumn(
                name: "HasLicense",
                table: "Shoppers",
                newName: "HasLicence");
        }
    }
}
