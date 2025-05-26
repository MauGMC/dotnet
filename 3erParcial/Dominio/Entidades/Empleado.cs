namespace Dominio.Entidades
{
    public class Empleado
    {
        //PK
        public int EmpleadoID { get; set; }
        //FKs
        public string UsuarioID { get; set; } = string.Empty;
        public int DireccionID { get; set; }
        //Atributos
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string? ApellidoMaterno { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public char? Sexo { get; set; }
        public string? Puesto { get; set; }
        public int Estado { get; set; }
        public DateTime? FechaContratacion { get; set; }
        //Propiedades de navegación EF
        public Usuario Usuario { get; set; } = new Usuario();
        public Direccion Direccion { get; set; } = new Direccion();
        public ICollection<Compra> Compras { get; set; } = new HashSet<Compra>();


    }
}
