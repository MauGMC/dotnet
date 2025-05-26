namespace Dominio.Entidades
{
    public class PermisoDeRol
    {
        //PK
        public int PermisoDeRolID { get; set; }
        //FKs
        public int PermisoID { get; set; }
        public int RolID { get; set; }
        //Propiedades de navegación EF
        public Rol Rol { get; set; } = new Rol();
        public Permiso Permiso { get; set; } = new Permiso(); 
    }
}
