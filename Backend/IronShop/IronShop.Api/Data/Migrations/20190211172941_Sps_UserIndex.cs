using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IronShop.Api.Migrations
{
    public partial class Sps_UserIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //TODO: Alternative for use IConfiguration in this class
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            //TODO: Alternative creating a Resources file in the project
            //var path = Path.Combine(Resources.PathScripts, "IndexUser.sql");

            var path = Path.Combine(configuration["PathScripts"], "IndexUser.sql");
            migrationBuilder.Sql(File.ReadAllText(path));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
