namespace Dominio.Entidades
{
    public class Rol
    {
        //PK
        public int RolID { get; set; }
        //Atributos
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        //Propiedades de navegación EF
        public ICollection<PermisoDeRol> PermisosRoles { get; set; } = new HashSet<PermisoDeRol>();
        public ICollection<RolDeUsuario> RolesDeUsuarios { get; set; } = new HashSet<RolDeUsuario>();
        //Constructores
        public Rol() { }
        public Rol(int rolId, string nombre, string descripcion, bool activo)
        {
            if (rolId < 0)
                throw new ArgumentOutOfRangeException("El ID del rol no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del rol no puede estar vacío.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción del rol no puede estar vacía.", nameof(descripcion));
            RolID = rolId;
            Nombre = nombre;
            Descripcion = descripcion;
            Activo = activo;
            FechaCreacion = DateTime.Now; // Por defecto, la fecha de creación es hoy
        }
    }
}
