using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ams_finstek_dotnet.Migrations
{
    public partial class InitialDb10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Users");
        }
    }
}
