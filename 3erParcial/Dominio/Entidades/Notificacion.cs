namespace Dominio.Entidades
{
    public class Notificacion
    {
        //PK
        public int NotificacionID { get; set; }
        //Atributos
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string? Tipo { get; set; }
        //Propiedades de navegación EF
        public ICollection <NotificacionPorUsuario> NotificacionesPorUsuarios { get; set; } = new HashSet<NotificacionPorUsuario>();
        // Constructores
        public Notificacion() { }
        public Notificacion(int notificacionId, string titulo, string mensaje, string? tipo = null)
        {
            if (notificacionId < 0)
                throw new ArgumentOutOfRangeException("El ID de la notificación no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título no puede estar vacío.", nameof(titulo));
            if (string.IsNullOrWhiteSpace(mensaje))
                throw new ArgumentException("El mensaje no puede estar vacío.", nameof(mensaje));

            NotificacionID = notificacionId;
            Titulo = titulo;
            Mensaje = mensaje;
            Fecha = DateTime.Now;
            Tipo = tipo;
        }

    }
}
