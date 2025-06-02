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
        // Constructores
        public Pedido() { }
        public Pedido(int pedidoId, int clienteId, int direccionId,
            int estado, decimal totalSinIVA, string? comentarios = null)
        {
            if (pedidoId < 0)
                throw new ArgumentOutOfRangeException("El ID del pedido no puede ser negativo.");
            if (clienteId < 0)
                throw new ArgumentOutOfRangeException("El ID del cliente no puede ser negativo.");
            if (direccionId < 0)
                throw new ArgumentOutOfRangeException("El ID de la dirección no puede ser negativo.");
            if (estado < 0 || estado > 4) // Asumiendo que el estado es un entero entre 0 y 4
                throw new ArgumentOutOfRangeException("El estado del pedido está fuera de rango.");
            if (totalSinIVA < 0)
                throw new ArgumentOutOfRangeException("Los totales no pueden ser negativos.");
            PedidoID = pedidoId;
            ClienteID = clienteId;
            DireccionID = direccionId;
            FechaPedido = DateTime.Now;
            FechaEntrega = DateTime.Now.AddDays(7);
            Estado = estado;
            TotalSinIVA = totalSinIVA;
            IVA = 0.16M*totalSinIVA;
            TotalConIVA = totalSinIVA*1.16M;
            Comentarios = comentarios;
        }
    }
}
