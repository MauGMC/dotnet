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
        //Constructores
        public Usuario() { }
        public Usuario(string usuarioId, string nombreUsuario, Email email, string contrasena,
            DateTime fechaCreacion, bool activo = true, string? rutaImagenPerfil = null)
        {
            if (string.IsNullOrWhiteSpace(usuarioId))
                throw new ArgumentException("El ID del usuario no puede estar vacío.", nameof(usuarioId));
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(nombreUsuario));
           if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(contrasena));
            UsuarioID = usuarioId;
            NombreUsuario = nombreUsuario;
            Email = email;
            Contrasena = contrasena;
            FechaCreacion = fechaCreacion;
            UltimoAcceso = null; // Por defecto, último acceso es nulo
            Activo = activo;
            RutaImagenPerfil = rutaImagenPerfil;
        }
    }
}
