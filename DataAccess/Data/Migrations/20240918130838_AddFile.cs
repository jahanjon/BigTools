using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Brands");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Brands",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FileExtension = table.Column<string>(type: "text", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_OwnerId",
                table: "Brands",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Suppliers_OwnerId",
                table: "Brands",
                column: "OwnerId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Suppliers_OwnerId",
                table: "Brands");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Brands_OwnerId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Brands",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
