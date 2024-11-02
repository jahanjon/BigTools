using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSupplierNationalIdLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Constant.SupplierListViewDrop);
            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Suppliers",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
            migrationBuilder.Sql(Constant.SupplierListViewCreate);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Constant.SupplierListViewDrop);
            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Suppliers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(11)",
                oldMaxLength: 11);
            migrationBuilder.Sql(Constant.SupplierListViewCreate);
        }
    }
}
