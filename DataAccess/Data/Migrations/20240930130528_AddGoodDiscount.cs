using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGoodDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    ConditionDescription = table.Column<string>(type: "text", nullable: false),
                    SaleType = table.Column<int>(type: "integer", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    PaymentDurationDays = table.Column<int>(type: "integer", nullable: true),
                    AmountMaxLimit = table.Column<double>(type: "double precision", nullable: true),
                    AmountMinLimit = table.Column<double>(type: "double precision", nullable: true),
                    CostMinLimit = table.Column<double>(type: "double precision", nullable: true),
                    ShopperRankLimit = table.Column<int>(type: "integer", nullable: true),
                    InvoiceDiscountPercent = table.Column<int>(type: "integer", nullable: true),
                    GoodDiscountPercent = table.Column<int>(type: "integer", nullable: true),
                    GiftItem = table.Column<string>(type: "text", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodDiscounts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodCodeGoodDiscounts",
                columns: table => new
                {
                    GoodDiscountId = table.Column<int>(type: "integer", nullable: false),
                    GoodsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodCodeGoodDiscounts", x => new { x.GoodDiscountId, x.GoodsId });
                    table.ForeignKey(
                        name: "FK_GoodCodeGoodDiscounts_GoodCodes_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "GoodCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodCodeGoodDiscounts_GoodDiscounts_GoodDiscountId",
                        column: x => x.GoodDiscountId,
                        principalTable: "GoodDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodCodeGoodDiscounts_GoodsId",
                table: "GoodCodeGoodDiscounts",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodDiscounts_SupplierId",
                table: "GoodDiscounts",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodCodeGoodDiscounts");

            migrationBuilder.DropTable(
                name: "GoodDiscounts");
        }
    }
}
