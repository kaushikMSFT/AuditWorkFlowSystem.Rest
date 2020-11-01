using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditorAPI.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Portfolios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Portfolios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Portfolios",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportReleaseDate",
                table: "Portfolios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ReportReleaseDate",
                table: "Portfolios");
        }
    }
}
