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
        //Constructores
        public Permiso() { }
        public Permiso(int permisoId, string nombre, string descripcion)
        {
            if (permisoId < 0)
                throw new ArgumentOutOfRangeException("El ID del permiso no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del permiso no puede estar vacío.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción del permiso no puede estar vacía.", nameof(descripcion));
            PermisoID = permisoId;
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}
