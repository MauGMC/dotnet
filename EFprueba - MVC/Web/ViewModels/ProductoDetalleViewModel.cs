using Aplicacion.DTOs;
using System.ComponentModel.DataAnnotations;
using Web.Validaciones;

namespace Web.ViewModels
{
    public class ProductoDetalleViewModel 
    {
        public string? Accion { get; set; }
        public int ProductoID { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(30, ErrorMessage = "El nombre no puede exceder los 30 caracteres.")]
        public string Nombre { get; set; }

        [MaxLength(100), MinLength(5, ErrorMessage = "La descripción debe tener al menos 5 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
        [Range(1, 100, ErrorMessage = "La cantidad debe ser positiva.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Display(Name = "Fecha de ingreso")]
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        [DataType(DataType.Date)]
        [TodayOnwards(ErrorMessage = "La fecha de creación no puede ser mayor a la fecha actual.")]
        [MinDate("2000-01-01", ErrorMessage = "La fecha de creación no puede ser menor al año 2000.")]
        public DateTime FechaCreacion { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El campo Categoria es obligatorio.")]
        [Range(1, 10, ErrorMessage = "Elemento no válido")]
        public int Categoria { get; set; }

        [MaxFileSize(2, ErrorMessage = "El tamaño máximo permitido es de 2 MB.")]
        [AllowedExtensions(new[] { ".jpg",",jpeg", ".png", ".webp" }, ErrorMessage = "Solo se permiten archivos de tipo jpg, jpeg, png y webp.")]
        public IFormFile? Imagen { get; set; }
        public string? RutaImagen { get; set; }
    }
}
