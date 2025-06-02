namespace Aplicacion.DTOs
{
    public class ProductoImportadoDTO
    {
        public ProductoDetalleDTO Producto { get; set; }
        public List<string> Exitos { get; set; } = new();
        public List<string> Advertencias { get; set; } = new();
        public List<string> Errores { get; set; } = new();
        public bool Agregado { get; set; } = false;
    }
}
