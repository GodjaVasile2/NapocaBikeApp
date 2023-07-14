using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NapocaBike.Migrations
{
    public partial class blockundo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Member");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Member",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
