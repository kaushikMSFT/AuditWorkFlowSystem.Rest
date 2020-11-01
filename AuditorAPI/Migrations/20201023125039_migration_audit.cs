using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditorAPI.Migrations
{
    public partial class migration_audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "Portfolios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "Portfolios");
        }
    }
}
