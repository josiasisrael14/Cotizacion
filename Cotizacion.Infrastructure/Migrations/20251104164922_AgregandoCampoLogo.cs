using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cotizacion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoCampoLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Proformas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Proformas");
        }
    }
}
