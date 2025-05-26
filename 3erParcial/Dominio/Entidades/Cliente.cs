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
        public Carrito Carrito { get; set; } = new Carrito();
        public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();
        public ICollection<Direccion> Direcciones { get; set; } = new HashSet<Direccion>();
        public ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
        public ICollection<MetodoDePago> MetodosDePago { get; set; } = new HashSet<MetodoDePago>();


    }
}
