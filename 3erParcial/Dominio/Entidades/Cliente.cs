namespace Dominio.Entidades
{
    public class Cliente
    {
        //PK
        public int ClienteID { get; set; }
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
        //Propiedades de navegación EF
        public Usuario Usuario { get; set; } = new Usuario();
        public Carrito Carrito { get; set; } 
        public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();
        public ICollection<Direccion> Direcciones { get; set; } = new HashSet<Direccion>();
        public ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
        public ICollection<MetodoDePago> MetodosDePago { get; set; } = new HashSet<MetodoDePago>();
        //Constructores
        public Cliente() { }
        public Cliente(int clienteId, string userId, int dirrecionId, string nombres,
            string apellidoP, string apellidoM, string telefono, DateTime fechaNacimiento, char sexo)
        {
            var fechaMinima = DateTime.Now.AddYears(-18);
            if (clienteId < 0)
                throw new ArgumentOutOfRangeException("El ID del cliente no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("El ID del usuario no puede estar vacío.", nameof(userId));
            if (dirrecionId < 0)
                throw new ArgumentOutOfRangeException("El ID de la dirección no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(nombres))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombres));
            if (string.IsNullOrWhiteSpace(apellidoP))
                throw new ArgumentException("El apellido paterno no puede estar vacío.", nameof(apellidoP));
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono no puede estar vacío.", nameof(telefono));
            if (fechaNacimiento > DateTime.Now)
                throw new ArgumentOutOfRangeException("La fecha de nacimiento no puede ser futura.");
            if(fechaNacimiento < fechaMinima)
                throw new ArgumentOutOfRangeException("El cliente debe ser mayor de 18 años.");
            if (sexo != 'M' && sexo != 'F' && sexo != null)
                throw new ArgumentException("El sexo debe ser 'M' (masculino), 'F' (femenino) o nulo.", nameof(sexo));
            ClienteID = clienteId;
            UsuarioID = userId;
            DireccionID = dirrecionId;
            Nombres = nombres;
            ApellidoPaterno = apellidoP;
            ApellidoMaterno = apellidoM;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
        }

    }
}
