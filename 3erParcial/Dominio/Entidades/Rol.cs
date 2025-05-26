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
    }
}
