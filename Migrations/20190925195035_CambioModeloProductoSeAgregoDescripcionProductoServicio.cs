using Microsoft.EntityFrameworkCore.Migrations;

namespace UGOCPBackEnd2019.Migrations
{
    public partial class CambioModeloProductoSeAgregoDescripcionProductoServicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripcionProductoServicio",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionProductoServicio",
                table: "Product");
        }
    }
}
