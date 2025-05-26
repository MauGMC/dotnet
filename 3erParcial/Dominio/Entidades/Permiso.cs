namespace Dominio.Entidades
{
    public class Permiso
    {
        //PK
        public int PermisoID { get; set; }
        //Atributos
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        //Propiedades de navegación EF
        public ICollection<PermisoDeRol> PermisoDeRoles { get; set; } = new HashSet<PermisoDeRol>();
    }
}
