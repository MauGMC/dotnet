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
        //Constructores
        public PermisoDeRol() { }
        public PermisoDeRol(int permisoDeRolID, int permisoID, int rolID)
        {
            if (permisoDeRolID < 0)
                throw new ArgumentOutOfRangeException("El ID del permiso de rol no puede ser negativo.", nameof(permisoDeRolID));
            if (permisoID < 0)
                throw new ArgumentOutOfRangeException("El ID del permiso no puede ser negativo.", nameof(permisoID));
            if (rolID < 0)
                throw new ArgumentOutOfRangeException("El ID del rol no puede ser negativo.", nameof(rolID));
            PermisoDeRolID = permisoDeRolID;
            PermisoID = permisoID;
            RolID = rolID;
        }
    }
}
