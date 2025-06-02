namespace Dominio.Entidades
{
    public class NotificacionPorUsuario
    {
        //PK
        public int NotificacionPorUsuarioID { get; set; }
        //FKs
        public int NotificacionID { get; set; }
        public string UsuarioID { get; set; }
        //Atributos
        public bool Leido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaLeida { get; set; }
        //Propiedades de navegación EF
        public Notificacion Notificacion { get; set; } = new Notificacion();
        public Usuario Usuario { get; set; } = new Usuario();
        //Constructores
        public NotificacionPorUsuario() { }
        public NotificacionPorUsuario(int notificacionPorUsuarioId, int notificacionId, 
            string usuarioId, bool leido)
        {
            if (notificacionPorUsuarioId < 0)
                throw new ArgumentOutOfRangeException("El ID de la notificación por usuario no puede ser negativo.");
            if (notificacionId < 0)
                throw new ArgumentOutOfRangeException("El ID de la notificación no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(usuarioId))
                throw new ArgumentException("El ID del usuario no puede estar vacío.", nameof(usuarioId));
            NotificacionPorUsuarioID = notificacionPorUsuarioId;
            NotificacionID = notificacionId;
            UsuarioID = usuarioId;
            Leido = leido;
            FechaCreacion = DateTime.Now;
        }

    }
}
