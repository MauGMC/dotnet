namespace Aplicacion.DTOs
{
    public class ResultadoImportacionCsvDTO
    {
        public List<ProductoImportadoDTO> ProductosImportados { get; set; } = new();
        public List<string> ErroresGlobales { get; set; } = new();
    }
}
