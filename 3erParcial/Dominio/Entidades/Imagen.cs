namespace Dominio.Entidades
{
    public class Imagen
    {
        public int ImagenID { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string RutaImagen { get; set; } = string.Empty;
        public string TablaOrigen { get; set; } = string.Empty;
        public string IDOrigen { get; set; } = string.Empty;    
    }
}
