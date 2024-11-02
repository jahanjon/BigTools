using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_SubCategories_SubCategoryId",
                table: "Goods");

            migrationBuilder.DropTable(
                name: "MainCategoryShoppers");

            migrationBuilder.DropTable(
                name: "MainCategorySuppliers");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "MainCategories");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "Goods",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Goods_SubCategoryId",
                table: "Goods",
                newName: "IX_Goods_CategoryId");

            migrationBuilder.CreateTable(
                name: "CategoryLevel1s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLevel1s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLevel1Shoppers",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    ShoppersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLevel1Shoppers", x => new { x.CategoriesId, x.ShoppersId });
                    table.ForeignKey(
                        name: "FK_CategoryLevel1Shoppers_CategoryLevel1s_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "CategoryLevel1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryLevel1Shoppers_Shoppers_ShoppersId",
                        column: x => x.ShoppersId,
                        principalTable: "Shoppers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLevel1Suppliers",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    SuppliersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLevel1Suppliers", x => new { x.CategoriesId, x.SuppliersId });
                    table.ForeignKey(
                        name: "FK_CategoryLevel1Suppliers_CategoryLevel1s_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "CategoryLevel1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryLevel1Suppliers_Suppliers_SuppliersId",
                        column: x => x.SuppliersId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLevel2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLevel2s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryLevel2s_CategoryLevel1s_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "CategoryLevel1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLevel3s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLevel3s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryLevel3s_CategoryLevel2s_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "CategoryLevel2s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLevel1Shoppers_ShoppersId",
                table: "CategoryLevel1Shoppers",
                column: "ShoppersId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLevel1Suppliers_SuppliersId",
                table: "CategoryLevel1Suppliers",
                column: "SuppliersId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLevel2s_ParentCategoryId",
                table: "CategoryLevel2s",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLevel3s_ParentCategoryId",
                table: "CategoryLevel3s",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_CategoryLevel3s_CategoryId",
                table: "Goods",
                column: "CategoryId",
                principalTable: "CategoryLevel3s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_CategoryLevel3s_CategoryId",
                table: "Goods");

            migrationBuilder.DropTable(
                name: "CategoryLevel1Shoppers");

            migrationBuilder.DropTable(
                name: "CategoryLevel1Suppliers");

            migrationBuilder.DropTable(
                name: "CategoryLevel3s");

            migrationBuilder.DropTable(
                name: "CategoryLevel2s");

            migrationBuilder.DropTable(
                name: "CategoryLevel1s");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Goods",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Goods_CategoryId",
                table: "Goods",
                newName: "IX_Goods_SubCategoryId");

            migrationBuilder.CreateTable(
                name: "MainCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainCategoryShoppers",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    ShoppersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategoryShoppers", x => new { x.CategoriesId, x.ShoppersId });
                    table.ForeignKey(
                        name: "FK_MainCategoryShoppers_MainCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainCategoryShoppers_Shoppers_ShoppersId",
                        column: x => x.ShoppersId,
                        principalTable: "Shoppers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainCategorySuppliers",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    SuppliersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategorySuppliers", x => new { x.CategoriesId, x.SuppliersId });
                    table.ForeignKey(
                        name: "FK_MainCategorySuppliers_MainCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainCategorySuppliers_Suppliers_SuppliersId",
                        column: x => x.SuppliersId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MainCategoryId = table.Column<int>(type: "integer", nullable: true),
                    UpperSubCategoryId = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubCategories_SubCategories_UpperSubCategoryId",
                        column: x => x.UpperSubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainCategoryShoppers_ShoppersId",
                table: "MainCategoryShoppers",
                column: "ShoppersId");

            migrationBuilder.CreateIndex(
                name: "IX_MainCategorySuppliers_SuppliersId",
                table: "MainCategorySuppliers",
                column: "SuppliersId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_MainCategoryId",
                table: "SubCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_UpperSubCategoryId",
                table: "SubCategories",
                column: "UpperSubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_SubCategories_SubCategoryId",
                table: "Goods",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
