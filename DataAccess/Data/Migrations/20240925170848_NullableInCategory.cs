using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class NullableInCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_MainCategories_MainCategoryId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_SubCategories_UpperSubCategoryId",
                table: "SubCategories");

            migrationBuilder.AlterColumn<int>(
                name: "UpperSubCategoryId",
                table: "SubCategories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "MainCategoryId",
                table: "SubCategories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_MainCategories_MainCategoryId",
                table: "SubCategories",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_SubCategories_UpperSubCategoryId",
                table: "SubCategories",
                column: "UpperSubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_MainCategories_MainCategoryId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_SubCategories_UpperSubCategoryId",
                table: "SubCategories");

            migrationBuilder.AlterColumn<int>(
                name: "UpperSubCategoryId",
                table: "SubCategories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MainCategoryId",
                table: "SubCategories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_MainCategories_MainCategoryId",
                table: "SubCategories",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_SubCategories_UpperSubCategoryId",
                table: "SubCategories",
                column: "UpperSubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
