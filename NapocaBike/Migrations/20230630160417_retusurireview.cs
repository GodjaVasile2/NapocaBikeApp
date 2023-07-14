using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NapocaBike.Migrations
{
    public partial class retusurireview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberName",
                table: "Review");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Review");

            migrationBuilder.AddColumn<string>(
                name: "MemberName",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
