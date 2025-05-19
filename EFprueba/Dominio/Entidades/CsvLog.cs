namespace Dominio.Entidades
{
    public class CsvLog
    {
        public int CsvLogID { get; set; }
        public byte[] Archivo { get; set; } 
        public string? Usuario { get; set; }
        public string NombreArchivo { get; set; }        
        public DateTime FechaImportacion { get; set; } 
        public int ProductosImportados { get; set; }
        public int ProductosConAdvertencias { get; set; }
        public int ProductosConErrores { get; set; }
        public string? Observaciones { get; set; }
    }
}
