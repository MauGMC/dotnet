using System.ComponentModel.DataAnnotations;

namespace Aplicacion.DTOs
{
    public class FiltroProductoDTO
    {
        public string? Nombre { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Categoría no válida.")]
        public int? Categoria { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Precio mínimo inválido.")]
        public decimal? PrecioMinimo { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Precio máximo inválido.")]
        public decimal? PrecioMaximo { get; set; }
        [Range(0, 1, ErrorMessage = "Estado de stock inválido.")]
        public bool? EstadoDelStock { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad mínima muy baja.")]
        public int? CantidadMinima { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad máxima muy alta.")]
        public int? CantidadMaxima { get; set; }
        [Range(0, 1, ErrorMessage = "Forma de ordenación inválida.")]
        public bool AscendenteODescendente { get; set; } = true;
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/2010", "1/1/2100", ErrorMessage = "Fecha de creación no válida.")]
        public DateTime? FechaCreacionDesde { get; set; }
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/2010", "1/1/2100", ErrorMessage = "Fecha de creación no válida.")]
        public DateTime? FechaCreacionHasta { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Número de página inválida.")]
        public int? Pagina { get; set; } = 1;
        [Range(0,int.MaxValue, ErrorMessage = "Cantidad por página inválida.")]
        public int? CantidadPorPagina { get; set; } = 10;
    }
}
