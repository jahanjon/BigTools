using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelationshipHistories_Relationships_RelationshipId",
                table: "RelationshipHistories");

            migrationBuilder.AddColumn<string>(
                name: "AcceptorNationalId",
                table: "Relationships",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequesterNationalId",
                table: "Relationships",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RelationshipId",
                table: "RelationshipHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationshipHistories_Relationships_RelationshipId",
                table: "RelationshipHistories",
                column: "RelationshipId",
                principalTable: "Relationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelationshipHistories_Relationships_RelationshipId",
                table: "RelationshipHistories");

            migrationBuilder.DropColumn(
                name: "AcceptorNationalId",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "RequesterNationalId",
                table: "Relationships");

            migrationBuilder.AlterColumn<int>(
                name: "RelationshipId",
                table: "RelationshipHistories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_RelationshipHistories_Relationships_RelationshipId",
                table: "RelationshipHistories",
                column: "RelationshipId",
                principalTable: "Relationships",
                principalColumn: "Id");
        }
    }
}
