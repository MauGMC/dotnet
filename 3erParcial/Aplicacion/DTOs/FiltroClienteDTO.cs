namespace Aplicacion.DTOs
{
    public class FiltroClienteDTO
    {
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaNacimientoDesde { get; set; }
        public DateTime? FechaNacimientoHasta { get; set; }
        public char? Sexo { get; set; } 
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
        public bool AscendenteODescendente { get; set; } = true;
        public int? Pagina { get; set; } = 1;
        public int? CantidadPorPagina { get; set; } = 10;
    }
}
