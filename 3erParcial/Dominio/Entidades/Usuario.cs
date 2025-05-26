using Dominio.ValueObjects;

namespace Dominio.Entidades
{
    public class Usuario
    {
        //PK
        public string UsuarioID { get; set; }
        //Atributos
        public string NombreUsuario { get; set; } = string.Empty;
        public Email Email { get; set; } 
        public string Contrasena { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public bool Activo { get; set; }
        public string? RutaImagenPerfil { get; set; }
        //Propiedades de navegación EF
        public Empleado Empleado { get; set; } = new Empleado();
        public Cliente Cliente { get; set; } = new Cliente();
        public ICollection<NotificacionPorUsuario> NotificacionesPorUsuarios { get; set; } = new HashSet<NotificacionPorUsuario>();
        public ICollection<RolDeUsuario> RolesDeUsuarios { get; set; } = new HashSet<RolDeUsuario>();
    }
}
