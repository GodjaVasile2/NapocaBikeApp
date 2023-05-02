using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NapocaBike.Migrations
{
    public partial class picture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikeRental");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Member",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Member");

            migrationBuilder.CreateTable(
                name: "BikeRental",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BikesAvailable = table.Column<int>(type: "int", nullable: false),
                    EmptySpaces = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeRental", x => x.ID);
                });
        }
    }
}
