using Microsoft.EntityFrameworkCore.Migrations;

namespace UGOCPBackEnd2019.Migrations
{
    public partial class CambiosModeloCompañia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdLocalidad",
                table: "Company",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLocalidad",
                table: "Company");
        }
    }
}
