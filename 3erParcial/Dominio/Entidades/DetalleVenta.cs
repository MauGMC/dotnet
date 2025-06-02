namespace Dominio.Entidades
{
    public class DetalleVenta
    {
        //PK
        public int DetalleVentaID { get; set; }
        //FKs
        public int VentaID { get; set; }
        public int ProductoID { get; set; }
        //Atributos
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        //Propiedades de navegación EF
        public Venta Venta { get; set; } = new Venta();
        public Producto Producto { get; set; } = new Producto();
        //Constructores
        public DetalleVenta() { }
        public DetalleVenta(int detalleVentaID, int ventaID, int productoID, int cantidad,
                            decimal precioUnitario)
        {
            if (detalleVentaID < 0)
                throw new ArgumentOutOfRangeException("El ID del detalle de venta no puede ser negativo.", nameof(detalleVentaID));
            if (ventaID < 0)
                throw new ArgumentOutOfRangeException("El ID de la venta no puede ser negativo.", nameof(ventaID));
            if (productoID < 0)
                throw new ArgumentOutOfRangeException("El ID del producto no puede ser negativo.", nameof(productoID));
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cantidad));
            if (precioUnitario <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor que cero.", nameof(precioUnitario));
            DetalleVentaID = detalleVentaID;
            VentaID = ventaID;
            ProductoID = productoID;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = cantidad * precioUnitario;
        }
    }
}
