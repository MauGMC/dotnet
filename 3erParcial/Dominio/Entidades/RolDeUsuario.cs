namespace Dominio.Entidades
{
    public class RolDeUsuario
    {
        //PK
        public int RolDeUsuarioID { get; set; }
        //FKs
        public string UsuarioID { get; set; }
        public int RolID { get; set; }
        //Propiedades de navegación EF
        public Usuario Usuario { get; set; } = new Usuario();
        public Rol Rol { get; set; } = new Rol();
        //Constructores
        public RolDeUsuario() { }
        public RolDeUsuario(int rolDeUsuarioId, string usuarioId, int rolId)
        {
            if (rolDeUsuarioId < 0)
                throw new ArgumentOutOfRangeException("El ID del rol de usuario no puede ser negativo.", nameof(rolDeUsuarioId));
            if (string.IsNullOrWhiteSpace(usuarioId))
                throw new ArgumentException("El ID del usuario no puede estar vacío.", nameof(usuarioId));
            if (rolId < 0)
                throw new ArgumentOutOfRangeException("El ID del rol no puede ser negativo.", nameof(rolId));
            RolDeUsuarioID = rolDeUsuarioId;
            UsuarioID = usuarioId;
            RolID = rolId;
        }
    }
}
