using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class EmailVO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "correo_electronico",
                table: "usuario",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "correo",
                table: "proveedores",
                newName: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "usuario",
                newName: "correo_electronico");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "proveedores",
                newName: "correo");
        }
    }
}
