using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddShopperListView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Constant.ShopperListViewCreate);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Constant.ShopperListViewDrop);
        }
    }
}
