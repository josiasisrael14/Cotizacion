using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cotizacion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoCampoPagoCredito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Credit",
                table: "Proformas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pay",
                table: "Proformas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Proformas");

            migrationBuilder.DropColumn(
                name: "Pay",
                table: "Proformas");
        }
    }
}
