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
    }
}
