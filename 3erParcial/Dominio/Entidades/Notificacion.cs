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


    }
}
