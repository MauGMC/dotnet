namespace Dominio.Entidades
{
    public class Pedido
    {
        //PK
        public int PedidoID { get; set; }
        //FKs
        public int ClienteID { get; set; }
        public int DireccionID { get; set; }
        //Atributos
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int Estado { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public decimal TotalSinIVA { get; set; }
        public decimal IVA { get; set; }
        public decimal TotalConIVA { get; set; }
        public string? Comentarios { get; set; }
        //Propiedades de navegación EF
        public Cliente Cliente { get; set; } = new Cliente();
        public Direccion Direccion { get; set; } = new Direccion();
        public ICollection<DetallePedido> DetallesPedidos { get; set; } = new HashSet<DetallePedido>();

    }
}
