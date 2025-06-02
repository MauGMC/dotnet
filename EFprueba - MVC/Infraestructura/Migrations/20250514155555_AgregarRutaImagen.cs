using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRutaImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "Categoria",
                table: "ProductosEF",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uint");

            migrationBuilder.AlterColumn<uint>(
                name: "Cantidad",
                table: "ProductosEF",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uint");

            migrationBuilder.AlterColumn<uint>(
                name: "ProductoID",
                table: "ProductosEF",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "RutaImagen",
                table: "ProductosEF",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RutaImagen",
                table: "ProductosEF");

            migrationBuilder.AlterColumn<int>(
                name: "Categoria",
                table: "ProductosEF",
                type: "uint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "ProductosEF",
                type: "uint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "ProductoID",
                table: "ProductosEF",
                type: "uint",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
