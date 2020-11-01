using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditorAPI.Migrations
{
    public partial class migration_audit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReportReleaseDate",
                table: "Portfolios",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Portfolios",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AuditorFirmId",
                table: "Portfolios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditorFirmId",
                table: "Portfolios");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReportReleaseDate",
                table: "Portfolios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Portfolios",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
