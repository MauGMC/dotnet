namespace Dominio.Entidades
{
    public class DetallePedido
    {
        //PK
        public int DetallePedidoID { get; set; }
        //FKs
        public int PedidoID { get; set; }
        public int ProductoID { get; set; }
        //Atributos
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        //Propiedades de navegación EF
        public Pedido Pedido { get; set; } = new Pedido();
        public Producto Producto { get; set; } = new Producto();
        //Constructores
        public DetallePedido() { }
        public DetallePedido(int detallePedidoID, int pedidoID, int productoID, int cantidad,
                             decimal precioUnitario)
        {
            if (detallePedidoID < 0)
                throw new ArgumentOutOfRangeException("El ID del detalle de pedido no puede ser negativo.", nameof(detallePedidoID));
            if (pedidoID < 0)
                throw new ArgumentOutOfRangeException("El ID del pedido no puede ser negativo.", nameof(pedidoID));
            if (productoID < 0)
                throw new ArgumentOutOfRangeException("El ID del producto no puede ser negativo.", nameof(productoID));
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cantidad));
            if (precioUnitario <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor que cero.", nameof(precioUnitario));
            DetallePedidoID = detallePedidoID;
            PedidoID = pedidoID;
            ProductoID = productoID;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = cantidad * precioUnitario;
        }
    }
}
