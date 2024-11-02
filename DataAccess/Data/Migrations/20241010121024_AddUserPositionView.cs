using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPositionView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Constant.UserPositionViewCreate);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Constant.UserPositionViewDrop);
        }
    }
}
