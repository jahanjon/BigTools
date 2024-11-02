using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class SupplierBrandModelEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierBrands",
                table: "SupplierBrands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierBrands",
                table: "SupplierBrands",
                columns: new[] { "SupplierId", "BrandId", "Type" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierBrands",
                table: "SupplierBrands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierBrands",
                table: "SupplierBrands",
                columns: new[] { "SupplierId", "BrandId" });
        }
    }
}
