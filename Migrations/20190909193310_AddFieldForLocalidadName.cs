using Microsoft.EntityFrameworkCore.Migrations;

namespace UGOCPBackEnd2019.Migrations
{
    public partial class AddFieldForLocalidadName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "localidad",
                table: "Company",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "localidad",
                table: "Company");
        }
    }
}
