namespace Dominio.Entidades
{
    public class Imagen
    {
        //PK
        public int ImagenID { get; set; }
        //Atributos
        public string NombreArchivo { get; set; } = string.Empty;
        public string RutaImagen { get; set; } = string.Empty;
        public string TablaOrigen { get; set; } = string.Empty;
        public string IDOrigen { get; set; } = string.Empty;
        //Constructores
        public Imagen() { }
        public Imagen(int imagenId, string nombreArchivo, string rutaImagen, string tablaOrigen, string idOrigen)
        {
            if (imagenId < 0)
                throw new ArgumentOutOfRangeException("El ID de la imagen no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(nombreArchivo))
                throw new ArgumentException("El nombre del archivo no puede estar vacío.", nameof(nombreArchivo));
            if (string.IsNullOrWhiteSpace(rutaImagen))
                throw new ArgumentException("La ruta de la imagen no puede estar vacía.", nameof(rutaImagen));
            if (string.IsNullOrWhiteSpace(tablaOrigen))
                throw new ArgumentException("La tabla de origen no puede estar vacía.", nameof(tablaOrigen));
            if (string.IsNullOrWhiteSpace(idOrigen))
                throw new ArgumentException("El ID de origen no puede estar vacío.", nameof(idOrigen));
            ImagenID = imagenId;
            NombreArchivo = nombreArchivo;
            RutaImagen = rutaImagen;
            TablaOrigen = tablaOrigen;
            IDOrigen = idOrigen;
        }
    }
}
