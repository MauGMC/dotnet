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

    }
}
