using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditorAPI.Migrations
{
    public partial class AuditorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Portfolios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuditProfiles",
                columns: table => new
                {
                    AuditProfileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditProfiles", x => x.AuditProfileId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditProfiles");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Portfolios");
        }
    }
}
