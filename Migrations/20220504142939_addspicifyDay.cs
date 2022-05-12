using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_API.Migrations
{
    public partial class addspicifyDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecfiyDay",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpecfiyDay",
                table: "AspNetUsers");
        }
    }
}
