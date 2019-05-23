using Microsoft.EntityFrameworkCore.Migrations;

namespace IronShop.Api.Migrations
{
    public partial class custommenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "PermissionMenuItems");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Menus",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Menus");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "PermissionMenuItems",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
