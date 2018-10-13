using Microsoft.EntityFrameworkCore.Migrations;

namespace EPAM.CoreWorkshop.NorthwindLib.Migrations
{
    public partial class AddCategoryOwnerisGay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Gay",
                table: "Categories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gay",
                table: "Categories");
        }
    }
}
