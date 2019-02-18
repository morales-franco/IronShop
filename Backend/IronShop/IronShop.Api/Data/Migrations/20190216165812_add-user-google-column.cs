using Microsoft.EntityFrameworkCore.Migrations;

namespace IronShop.Api.Migrations
{
    public partial class addusergooglecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GoogleAuth",
                table: "User",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleAuth",
                table: "User");
        }
    }
}
