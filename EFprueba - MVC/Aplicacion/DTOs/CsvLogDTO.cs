using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class CsvLogDTO
    {
        public int CsvLogID { get; set; }
        public byte[] Archivo { get; set; }
        public string NombreArchivo { get; set; }
        public string? Usuario { get; set; }
        public DateTime FechaImportacion { get; set; }
        public int Exitos { get; set; }
        public int Advertencias { get; set; }
        public int Errores { get; set; }
        public string? Observaciones { get; set; }
    }
}
