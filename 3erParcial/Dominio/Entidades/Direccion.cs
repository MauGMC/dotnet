namespace Dominio.Entidades
{
    public class Direccion
    {
        //PK
        public int DireccionID { get; set; }
        //Atributos
        public string Calle { get; set; } = string.Empty;
        public int NumeroInterior { get; set; }
        public int NumeroExterior { get; set; }
        public string Colonia { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public string? Telefono { get; set; }
        //Propiedades de navegación EF
        public Cliente Cliente { get; set; } = new Cliente();
        public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();
        public ICollection<Empleado> Empleados { get; set; } = new HashSet<Empleado>();
        // Constructores
        public Direccion() { }
        public Direccion(int direccionId, string calle, int numeroInterior, int numeroExterior,
            string colonia, string codigoPostal, string ciudad, string estado, string? referencia = null,
            string? telefono = null)
        {
            if (direccionId < 0)
                throw new ArgumentOutOfRangeException("El ID de la dirección no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(calle))
                throw new ArgumentException("La calle no puede estar vacía.", nameof(calle));
            if (numeroInterior < 0)
                throw new ArgumentOutOfRangeException("El número interior no puede ser negativo.");
            if (numeroExterior < 0)
                throw new ArgumentOutOfRangeException("El número exterior no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(colonia))
                throw new ArgumentException("La colonia no puede estar vacía.", nameof(colonia));
            if (string.IsNullOrWhiteSpace(codigoPostal))
                throw new ArgumentException("El código postal no puede estar vacío.", nameof(codigoPostal));
            if (string.IsNullOrWhiteSpace(ciudad))
                throw new ArgumentException("La ciudad no puede estar vacía.", nameof(ciudad));
            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("El estado no puede estar vacío.", nameof(estado));
            DireccionID = direccionId;
            Calle = calle;
            NumeroInterior = numeroInterior;
            NumeroExterior = numeroExterior;
            Colonia = colonia;
            CodigoPostal = codigoPostal;
            Ciudad = ciudad;
            Estado = estado;
            Referencia = referencia;
            Telefono = telefono;
        }

    }
}
