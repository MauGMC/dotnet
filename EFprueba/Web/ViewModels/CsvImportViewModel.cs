using System.ComponentModel.DataAnnotations;
using Web.Validaciones;

namespace Web.ViewModels
{
    public class CsvImportViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar un archivo.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".csv" }, ErrorMessage = "Solo se permiten archivos de tipo csv.")]
        [Display(Name = "Suba un archivo...")]
        [NotEmptyFile(ErrorMessage = "El archivo no puede estar vacío.")]
        public IFormFile ArchivoCsv { get; set; }

    }
}
