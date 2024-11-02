using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequesterId = table.Column<int>(type: "integer", nullable: false),
                    RequesterName = table.Column<string>(type: "text", nullable: false),
                    RequesterType = table.Column<int>(type: "integer", nullable: false),
                    RequesterCityId = table.Column<int>(type: "integer", nullable: false),
                    RequesterPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    AcceptorId = table.Column<int>(type: "integer", nullable: true),
                    AcceptorName = table.Column<string>(type: "text", nullable: false),
                    AcceptorType = table.Column<int>(type: "integer", nullable: false),
                    AcceptorCityId = table.Column<int>(type: "integer", nullable: false),
                    AcceptorPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    RelationshipType = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    HasKnown = table.Column<bool>(type: "boolean", nullable: true),
                    HasKnownFromYear = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HasTrade = table.Column<bool>(type: "boolean", nullable: true),
                    HasTradeFromYear = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Suggests = table.Column<bool>(type: "boolean", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PerformerType = table.Column<int>(type: "integer", nullable: false),
                    PerformerId = table.Column<int>(type: "integer", nullable: false),
                    NewState = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RelationshipId = table.Column<int>(type: "integer", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationshipHistories_Relationships_RelationshipId",
                        column: x => x.RelationshipId,
                        principalTable: "Relationships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipHistories_RelationshipId",
                table: "RelationshipHistories",
                column: "RelationshipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelationshipHistories");

            migrationBuilder.DropTable(
                name: "Relationships");
        }
    }
}
