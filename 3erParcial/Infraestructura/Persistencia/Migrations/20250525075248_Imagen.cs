using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class Imagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Imagenes",
                table: "Imagenes");

            migrationBuilder.RenameTable(
                name: "Imagenes",
                newName: "imagenes");

            migrationBuilder.RenameColumn(
                name: "TablaOrigen",
                table: "imagenes",
                newName: "tabla_origen");

            migrationBuilder.RenameColumn(
                name: "RutaImagen",
                table: "imagenes",
                newName: "ruta_imagen");

            migrationBuilder.RenameColumn(
                name: "NombreArchivo",
                table: "imagenes",
                newName: "producto_id");

            migrationBuilder.RenameColumn(
                name: "IDOrigen",
                table: "imagenes",
                newName: "id_origen");

            migrationBuilder.RenameColumn(
                name: "ImagenID",
                table: "imagenes",
                newName: "imagen_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_imagenes",
                table: "imagenes",
                column: "imagen_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_imagenes",
                table: "imagenes");

            migrationBuilder.RenameTable(
                name: "imagenes",
                newName: "Imagenes");

            migrationBuilder.RenameColumn(
                name: "tabla_origen",
                table: "Imagenes",
                newName: "TablaOrigen");

            migrationBuilder.RenameColumn(
                name: "ruta_imagen",
                table: "Imagenes",
                newName: "RutaImagen");

            migrationBuilder.RenameColumn(
                name: "producto_id",
                table: "Imagenes",
                newName: "NombreArchivo");

            migrationBuilder.RenameColumn(
                name: "id_origen",
                table: "Imagenes",
                newName: "IDOrigen");

            migrationBuilder.RenameColumn(
                name: "imagen_id",
                table: "Imagenes",
                newName: "ImagenID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Imagenes",
                table: "Imagenes",
                column: "ImagenID");
        }
    }
}
