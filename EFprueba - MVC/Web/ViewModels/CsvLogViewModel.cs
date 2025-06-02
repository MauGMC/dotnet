namespace Web.ViewModels
{
    public class CsvLogViewModel
    {
        public int CsvLogID { get; set; }
        public byte[] Archivo { get; set; }
        public string? Usuario { get; set; }
        public DateTime FechaImportacion { get; set; }
        public int Exitos { get; set; }
        public int Advertencias { get; set; }
        public int Errores { get; set; }
        public string? Observaciones { get; set; }

        public int PaginaActual { get; set; } = 1;
        public int TotalPaginas { get; set; }
    }
}
