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
        //Constructores
        public Empleado() { }
        public Empleado(int empleadoId, string userId, int direccionId, string nombres,
            string apellidoP, string apellidoM, string telefono, DateTime fechaNacimiento, char sexo,
            string? puesto = null, int estado = 1)
        {
            var fechaMinima = DateTime.Now.AddYears(-18);
            if (empleadoId < 0)
                throw new ArgumentOutOfRangeException("El ID del empleado no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("El ID del usuario no puede estar vacío.", nameof(userId));
            if (direccionId < 0)
                throw new ArgumentOutOfRangeException("El ID de la dirección no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(nombres))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombres));
            if (string.IsNullOrWhiteSpace(apellidoP))
                throw new ArgumentException("El apellido paterno no puede estar vacío.", nameof(apellidoP));
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono no puede estar vacío.", nameof(telefono));
            if (fechaNacimiento > DateTime.Now)
                throw new ArgumentOutOfRangeException("La fecha de nacimiento no puede ser futura.");
            if (fechaNacimiento < fechaMinima)
                throw new ArgumentOutOfRangeException("El empleado debe ser mayor de 18 años.");
            if (sexo != 'M' && sexo != 'F' && sexo != null)
                throw new ArgumentException("El sexo debe ser 'M' (masculino), 'F' (femenino) o nulo.", nameof(sexo));
            EmpleadoID = empleadoId;
            UsuarioID = userId;
            DireccionID = direccionId;
            Nombres = nombres;
            ApellidoPaterno = apellidoP;
            ApellidoMaterno = apellidoM;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
            Puesto = puesto;
            Estado = estado;
            FechaContratacion = DateTime.Now; // Por defecto, la fecha de contratación es hoy
        }


    }
}
